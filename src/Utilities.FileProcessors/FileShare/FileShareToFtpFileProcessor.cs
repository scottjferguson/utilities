using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Core.Application;
using Core.FileHandling;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Utilities.FileProcessors.Base;

namespace Utilities.FileProcessors.FileShare
{
    [Processor(Name = "FileShareToFtp")]
    public class FileShareToFtpFileProcessor : FileProcessorBase, IProcessor
    {
        private readonly List<string> _errors = new List<string>();

        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            string connectionString = "";
            string sourceFileShareName = "";
            string sourceDirectory = "";

            Iterate(connectionString, sourceFileShareName, sourceDirectory);

            if (_errors.Any())
            {
                foreach (string error in _errors)
                {
                    Console.Write(error);
                }
            }
        }

        public void Iterate(string connectionString, string shareName, string directory = null)
        {
            ShareClient share = new ShareClient(connectionString, shareName);

            // Track the remaining directories to walk, starting from the root
            var remaining = new Queue<ShareDirectoryClient>();

            if (directory == null)
            {
                remaining.Enqueue(share.GetRootDirectoryClient());
            }
            else
            {
                remaining.Enqueue(share.GetDirectoryClient(directory));
            }

            while (remaining.Count > 0)
            {
                // Get all of the next directory's files and subdirectories
                ShareDirectoryClient dir = remaining.Dequeue();

                foreach (ShareFileItem item in dir.GetFilesAndDirectories())
                {
                    if (!File.Exists(@"C:\audio\sales\trusted\Sales\" + item.Name))
                    {
                        string filepath = Download(connectionString, shareName, directory, item.Name);
                    }

                    //Upload(filepath);

                    // Keep walking down directories
                    if (item.IsDirectory)
                    {
                        remaining.Enqueue(dir.GetSubdirectoryClient(item.Name));
                    }
                }
            }
        }

        public string Download(string connectionString, string shareName, string directory, string fileName)
        {
            // Get a reference to the file
            ShareClient share = new ShareClient(connectionString, shareName);
            ShareDirectoryClient directoryClient = share.GetDirectoryClient(directory);
            ShareFileClient file = directoryClient.GetFileClient(fileName);

            string localFilePath = @"C:\audio\sales\trusted\Sales\" + file.Name;

            try
            {
                // Download the file
                ShareFileDownloadInfo download = file.Download();

                using (FileStream stream = File.OpenWrite(localFilePath))
                {
                    download.Content.CopyTo(stream);
                }
            }
            catch (Exception e)
            {
                _errors.Add(fileName + ": " + e.Message);
            }

            return localFilePath;
        }

        private void Upload(string filepath)
        {
            using (var ftp = new FtpClient("trieve.co", "compliance@trieve.co", "Greeting123"))
            {
                ftp.Connect();

                ftp.UploadFile(filepath, "/public_html/Operations/TPV/SVC/Audio/" + Path.GetFileName(filepath));
            }

            File.Delete(filepath);
        }

        public void UploadFile(Stream stream, string filePath)
        {
            stream.Position = 0;

            FtpWebRequest ftpWebRequest = CreateFtpRequest(filePath, WebRequestMethods.Ftp.UploadFile);

            using (Stream ftpStream = ftpWebRequest.GetRequestStream())
            {
                stream.CopyTo(ftpStream);
            }

            stream.Close();
        }

        private FtpWebRequest CreateFtpRequest(string filePath, string method)
        {
            string connectionString = "Host=trieve.co;Port=21;Username=compliance@trieve.co;Password=Greeting123;";

            var ftpClientSettings = new FtpClientSettings(connectionString);

            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(@"ftp://trieve.co/public_html/Operations/TPV/SVC/Audio");

            ftpWebRequest.Credentials = new NetworkCredential(ftpClientSettings.Username, ftpClientSettings.Password);

            ftpWebRequest.UseBinary = true;
            ftpWebRequest.UsePassive = true;
            ftpWebRequest.KeepAlive = true;

            ftpWebRequest.Method = method;

            return ftpWebRequest;
        }
    }
}
