

namespace TimeLinetoDB
{
    public interface SqlCmmandText
    {
        string CheckDb();
        string CreateDatabase();
        string CheckTables();
    }

    //MicrosoftSQL
    public class SqlCmmandTextMicrosoftSql : SqlCmmandText
    {
        public string CreateDatabase()
        {
            return @"
 CREATE DATABASE [multi_project]
 CONTAINMENT = NONE
 ON PRIMARY
 (NAME = N'multi_project', FILENAME = N'@DB_PATH', SIZE = 5120KB, FILEGROWTH = 1024KB )
 LOG ON
 (NAME = N'multi_project_log', FILENAME = N'@LOG_PATH', SIZE = 3048KB, FILEGROWTH = 10 %)
 GO
 ALTER DATABASE[multi_project] SET COMPATIBILITY_LEVEL = 120
 GO
 ALTER DATABASE[multi_project] SET ANSI_NULL_DEFAULT OFF
 GO
 ALTER DATABASE[multi_project] SET ANSI_NULLS OFF
 GO
 ALTER DATABASE[multi_project] SET ANSI_PADDING OFF
 GO
 ALTER DATABASE[multi_project] SET ANSI_WARNINGS OFF
 GO
 ALTER DATABASE[multi_project] SET ARITHABORT OFF
 GO
 ALTER DATABASE[multi_project] SET AUTO_CLOSE OFF
 GO
 ALTER DATABASE[multi_project] SET AUTO_SHRINK OFF
 GO
 ALTER DATABASE[multi_project] SET AUTO_CREATE_STATISTICS ON
 GO
 ALTER DATABASE[multi_project] SET AUTO_UPDATE_STATISTICS ON
 GO
 ALTER DATABASE[multi_project] SET CURSOR_CLOSE_ON_COMMIT OFF
 GO
 ALTER DATABASE[multi_project] SET CURSOR_DEFAULT  GLOBAL
 GO
 ALTER DATABASE[multi_project] SET CONCAT_NULL_YIELDS_NULL OFF
 GO
 ALTER DATABASE[multi_project] SET NUMERIC_ROUNDABORT OFF
 GO
 ALTER DATABASE[multi_project] SET QUOTED_IDENTIFIER OFF
 GO
 ALTER DATABASE[multi_project] SET RECURSIVE_TRIGGERS OFF
 GO
 ALTER DATABASE[multi_project] SET DISABLE_BROKER
 GO
 ALTER DATABASE[multi_project] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
 GO
 ALTER DATABASE[multi_project] SET DATE_CORRELATION_OPTIMIZATION OFF
 GO
 ALTER DATABASE[multi_project] SET PARAMETERIZATION SIMPLE
 GO
 ALTER DATABASE[multi_project] SET READ_COMMITTED_SNAPSHOT OFF
 GO
 ALTER DATABASE[multi_project] SET READ_WRITE
 GO
 ALTER DATABASE[multi_project] SET RECOVERY SIMPLE
 GO
 ALTER DATABASE[multi_project] SET MULTI_USER
 GO
 ALTER DATABASE[multi_project] SET PAGE_VERIFY CHECKSUM
 GO
 ALTER DATABASE[multi_project] SET TARGET_RECOVERY_TIME = 0 SECONDS
 GO
 ALTER DATABASE[multi_project] SET DELAYED_DURABILITY = DISABLED
 GO
 USE[multi_project]
 GO
 IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default = 1 AND name = N'PRIMARY') ALTER DATABASE[multi_project] MODIFY FILEGROUP[PRIMARY] DEFAULT
 GO
";
        }//end of CreateDatabas

        public string CheckDb()
        {
            return @"SELECT NAME FROM SYS.DATABASES WHERE NAME='multi_project'";
        }

        public string CheckTables()
        {
            return @"use multi_project; SELECT name FROM Sys.Tables;";
        }


    }
    //MySql
    public class SqlCmmandTextMySql : SqlCmmandText
    {
        public string CreateDatabase()
        {
            return "";
        }
        public string CheckDb()
        {
            return "";
        }
        public string CheckTables()
        {
            return "";
        }
    }
}
