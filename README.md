Prerequisites
===================================

The core SimSig data is stored in a SQL database with the location / path data stored in a Graph DB which was introduced with SQL2017. In order for the solution to build correctly and SQL Unit tests to run successfully you must be running a SQL 2019 localdb instance (SQL 2019 is needed to allow merging of Graph database records). Instructions of how to install and configure this can be found at https://knowledge-base.havit.eu/2018/09/04/sql-localdb-upgrade-to-2017-14-0-1000. The SQL2019 localdb install can be found at https://download.microsoft.com/download/7/c/1/7c14e92e-bdcb-4f89-b7cf-93543e7112d1/SqlLocalDB.msi
If you don't use the default instance of (localdb)\MSSQLLocalDB then you will need to change the config of the GroundFrame.SQL.UnitTests solution to point to your preferred instance and also update the settings xunit.config.json in the GroundFrame.Classes.UnitTests solution
