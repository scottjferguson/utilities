# How To

[Creating and Updating the entity model](https://www.learnentityframeworkcore.com/walkthroughs/existing-database)

### .NET Core CLI:

Install:
	Microsoft.EntityFrameworkCore
	Microsoft.EntityFrameworkCore.Design
	Microsoft.EntityFrameworkCore.SqlServer
	Microsoft.EntityFrameworkCore.Tools

Tools > Command Line > Developer Command Prompt

cd src\Utilities.Domain

// Run once per machine:
// dotnet tool install --global dotnet-ef --version 3.1.5
// ...or this to update it: dotnet tool update --global dotnet-ef

dotnet ef dbcontext scaffold "Data Source=guroo-server-dev.database.windows.net;Initial Catalog=Customer;User ID=GurooBatch;Password={{DatabasePassword}};Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;" Microsoft.EntityFrameworkCore.SqlServer -o Customer\Entities -f

**!! Be sure to remove the connection string from the generated DbContext method OnConfiguring(). It contains the password in plain text !!**

(if you get an error about updating EF Core tools, run this: dotnet tool update --global dotnet-ef)