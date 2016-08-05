using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLinetoDB
{
    public class DbController : DbControllerBase
    {


        #region DB Check
        public bool ConnectDBTest()
        {
            return ConnectDBReader(CheckDB, sql.CheckDb());
        }
        public Func<SqlDataReader, bool> CheckDB = (reader) =>
        {
            bool returnFlag = false;
            string name = null;
            while (reader.Read())
            {
                name = (string)reader.GetValue(0);
                if (name != null)
                {
                    returnFlag = true;
                }
            }
            return returnFlag;
        };
        #endregion


        #region DB Create
        private static string createDbPath;
        public bool DBCreate(string basePath)
        {
            createDbPath = basePath;
            return ConnectDBQuery(CreateDB, sql.CreateDatabase());
        }
        public Action<SqlCommand> CreateDB = (parameter) =>
        {
            string path = createDbPath + @"\MSSQL12.MSSQLSERVER\MSSQL\DATA\multi_project.mdf";
            string logPath = createDbPath + @"\MSSQL12.MSSQLSERVER\MSSQL\DATA\multi_project_log.ldf";
            parameter.CommandText = parameter.CommandText.Replace("@DB_PATH", path);
            parameter.CommandText = parameter.CommandText.Replace("@LOG_PATH", logPath);
            //parameter.Parameters.Add(new SqlParameter("@DB_PATH", @path));
            //parameter.Parameters.Add(new SqlParameter("@LOG_PATH", @logPath));

            //parameter.CommandText = @"INSERT INTO T_USER (ID, PASSWORD, ROLE_NAME) VALUES (@ID, @PASSWORD, @ROLE_NAME)";
            //parameter.Parameters.Add(new SqlParameter("@ID", id));
        };
        #endregion


        #region DB CheckTables
        public ArrayList DbCheckTables()
        {
            return ConnectDBReaderArray(CheckTables, sql.CheckTables());
        }

        public Func<SqlDataReader, ArrayList> CheckTables = (reader) =>
        {
            ArrayList returnData = new ArrayList();

            string name = null;
            while (reader.Read())
            {
                name = (string)reader.GetValue(0);
                if (name != null)
                {
                    returnData.Add(name);
                }
            }
            return returnData;
        };
        #endregion


    }


}

