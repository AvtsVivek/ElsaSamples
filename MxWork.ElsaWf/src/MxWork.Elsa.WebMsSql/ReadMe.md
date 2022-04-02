Need to configure an sql lite database

https://www.sqlite.org/download.html
https://www.sqlitetutorial.net/download-install-sqlite/
sqlite3 DatabaseName.db
SET EF_CONNECTIONSTRING=Data Source=c:\data\elsa-dashboard.db;Cache=Shared

https://elsa-workflows.github.io/elsa-core/docs/guides-dashboard

1. Ensure the package Microsoft.EntityFrameworkCore.Tools is installed.
2. Set the env variable EF_CONNECTIONSTRING as follows
	SET EF_CONNECTIONSTRING='Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElsaSqlDb5;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False'
	Set-Item -Path Env:EF_CONNECTIONSTRING -Value 'Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElsaSqlDb5;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False'
	Get-Item -Path Env:EF_CONNECTIONSTRING
	
3. Add-Migration -StartupProject MxWork.Elsa.WebMsSql -Context "SqlServerContext" InitialMigration
4. Or else this should also work. 
	Add-Migration InitialMigration -o SqlServerMigrations -Context SqlServerContext
5. Update-Database
