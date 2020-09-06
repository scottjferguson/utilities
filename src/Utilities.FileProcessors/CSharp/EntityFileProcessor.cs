using Core.Application;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.FileProcessors.Base;

namespace Utilities.FileProcessors.CSharp
{
    [Processor(Name = "Entity")]
    public class EntityFileProcessor : FileProcessorBase, IProcessor
    {
        private const string DirectoryPath = @"C:\Users\scott\source\repos\guroo\Customer\Customer\src\Domain\";
        private readonly ConcurrentDictionary<string, string> _entityTypeNameDictionary;
        private readonly string _entityDirectory;
        private readonly string _contextDirectory;

        public EntityFileProcessor()
        {
            _entityTypeNameDictionary = new ConcurrentDictionary<string, string>();
            _entityDirectory = Path.Combine(DirectoryPath, "Entities");
            _contextDirectory = Path.Combine(DirectoryPath, "Context");
        }

        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            string[] entityFilePaths = Directory.GetFiles(_entityDirectory);

            // Operate on the Entity class files first
            Parallel.ForEach(entityFilePaths, async (filePath, state) =>
            {
                FileInfo fileInfo = new FileInfo(filePath);

                string newFileContent;
                string oldEntityName = Path.GetFileNameWithoutExtension(fileInfo.Name);
                string newEntityName = $"{oldEntityName}Entity";
                string fileContent = await File.ReadAllTextAsync(fileInfo.FullName, cancellationToken);
                string searchTerm = $"public partial class {oldEntityName}";

                if (filePath.EndsWith("Entity.cs"))
                {
                    Console.WriteLine($"Ignoring existing entity class: {fileInfo.Name}");
                    state.Break();
                }

                if (fileContent.Contains(" : DbContext"))
                {
                    Console.WriteLine($"Ignoring DbContext: {fileInfo.Name}");
                    state.Break();
                }

                bool isView = oldEntityName.StartsWith("Vw");
                
                if (fileContent.Contains("public string CreatedBy { get; set; }") &&
                    fileContent.Contains("public DateTime CreatedDate { get; set; }") &&
                    fileContent.Contains("public string ModifiedBy { get; set; }") &&
                    fileContent.Contains("public DateTime? ModifiedDate { get; set; }") &&
                    !isView)
                {
                    // Rename the class with a suffix of Entity (this class has all auditable fields, make it IAuditable)
                    newFileContent = fileContent.Replace(searchTerm, $"{searchTerm}Entity : Core.Framework.IAuditable");
                }
                else if (fileContent.Contains("public string CreatedBy { get; set; }") &&
                         fileContent.Contains("public DateTime CreatedDate { get; set; }") &&
                         !isView)
                {
                    // Rename the class with a suffix of Entity (this class has only created fields, make it IReadOnly)
                    newFileContent = fileContent.Replace(searchTerm, $"{searchTerm}Entity : Core.Framework.IReadOnly");
                }
                else
                {
                    // Rename the class with a suffix of Entity
                    newFileContent = fileContent.Replace(searchTerm, $"{searchTerm}Entity");
                }

                // Rename the constructor
                newFileContent = newFileContent.Replace($"public {oldEntityName}()", $"public {oldEntityName}Entity()");

                // Overwrite the context file with the new content
                await File.WriteAllTextAsync(fileInfo.FullName, newFileContent, cancellationToken);

                // Track this entity name, we'll need it later
                _entityTypeNameDictionary.TryAdd(oldEntityName, newEntityName);
            });

            // We still need to update the the type names of the properties so that they reference the new entity names (we can reference _entityClassNames for a consolidated list of what has changed)
            Parallel.ForEach(entityFilePaths, async (filePath, state) =>
            {
                FileInfo fileInfo = new FileInfo(filePath);

                // Read it back as an array, we'll be doing line-by-line replacements from here
                string[] fileLines = await File.ReadAllLinesAsync(fileInfo.FullName, cancellationToken);

                // Keep the entire contents of the file in a string for the destination of the search and replaces
                string fileContent = await File.ReadAllTextAsync(fileInfo.FullName, cancellationToken);
                string newFileContent = fileContent;

                foreach (string fileLine in fileLines.Where(fl => fl.Contains("public virtual")))
                {
                    if (fileLine.Count(c => c == ' ') < 2)
                    {
                        Console.WriteLine($"Could not parse property type line for file line: {fileLine}");
                        continue;
                    }

                    // Parse the old type name from the line
                    string[] fileLineSegments = fileLine.Replace("ICollection", "").Replace("<", "").Replace(">", "").Split(" ");
                    string oldTypeName = fileLineSegments[Array.IndexOf(fileLineSegments, "virtual") + 1];

                    if (!_entityTypeNameDictionary.TryGetValue(oldTypeName, out string newTypeName))
                    {
                        Console.WriteLine($"Could not find matching type name reference in _entityTypeNameDictionary for type name {oldTypeName}");
                        continue;
                    }

                    // Replace the old type name with the new type name
                    newFileContent = newFileContent.Replace(fileLine, ReplaceFirst(fileLine, oldTypeName, newTypeName));
                }

                // Overwrite the context file with the new content
                await File.WriteAllTextAsync(fileInfo.FullName, newFileContent, cancellationToken);
            });

            // Rename the entity files
            Parallel.ForEach(entityFilePaths, async (filePath, state) =>
            {
                FileInfo fileInfo = new FileInfo(filePath);

                string oldTypeName = Path.GetFileNameWithoutExtension(fileInfo.Name);
                string fileContent = await File.ReadAllTextAsync(fileInfo.FullName, cancellationToken);

                if (!_entityTypeNameDictionary.TryGetValue(oldTypeName, out string newTypeName))
                {
                    Console.WriteLine($"Could not find matching type name reference in _entityTypeNameDictionary for type name {oldTypeName}");
                    state.Break();
                }

                string newFilePath = Path.Combine(fileInfo.DirectoryName, $"{newTypeName}.cs");

                // Write the new content to a new file with a name that matches the new entity type name
                await File.WriteAllTextAsync(newFilePath, fileContent, cancellationToken);

                // Delete the old file
                File.Delete(fileInfo.FullName);
            });

            // Operate on the Context class file
            string[] contextFilePaths = Directory.GetFiles(_contextDirectory).Where(path => path.EndsWith("Context.cs") && !path.EndsWith("DbContext.cs")).ToArray();

            if (contextFilePaths.Length == 0)
            {
                Console.WriteLine("No context files found. Could not update context class file");
            }
            else if (contextFilePaths.Length > 1)
            {
                Console.WriteLine("Multiple context files found. Could not update context class file");
            }
            else
            {
                FileInfo fileInfo = new FileInfo(contextFilePaths.Single());

                // Get the contents of the context file
                string fileContent = await File.ReadAllTextAsync(fileInfo.FullName, cancellationToken);
                string newFileContent = fileContent;

                // Replace type names
                foreach (KeyValuePair<string, string> entityClassName in _entityTypeNameDictionary)
                {
                    newFileContent = newFileContent.Replace($"<{entityClassName.Key}>", $"<{entityClassName.Key}Entity>");
                }

                // Overwrite the context file with the new content
                await File.WriteAllTextAsync(fileInfo.FullName, newFileContent, cancellationToken);

                // Read it back as an array, we'll be doing line-by-line replacements from here
                string[] contextFileLines = await File.ReadAllLinesAsync(fileInfo.FullName, cancellationToken);

                // Remove the password left behind be EF's generator
                string lineWithConnectionString = contextFileLines.SingleOrDefault(fl => fl.Contains("optionsBuilder.UseSqlServer("));
                string lineWithConnectionStringComment = contextFileLines.SingleOrDefault(fl => fl.Contains("#warning To protect potentially sensitive information in your connection string"));

                if (!string.IsNullOrEmpty(lineWithConnectionString) && !string.IsNullOrEmpty(lineWithConnectionStringComment))
                {
                    newFileContent = newFileContent
                        .Replace(lineWithConnectionString, "")
                        .Replace(lineWithConnectionStringComment, "");
                }

                // Overwrite the context file with the new content
                await File.WriteAllTextAsync(fileInfo.FullName, newFileContent, cancellationToken);
            }
        }
        
        private string ReplaceFirst(string str, string search, string replace)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            int pos = str.IndexOf(search);

            if (pos < 0)
            {
                return str;
            }

            return str.Substring(0, pos) + replace + str.Substring(pos + search.Length);
        }
    }
}
