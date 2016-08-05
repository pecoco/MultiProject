using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace TimeLinetoDB
{
    public class DbControllerBase
    {
        protected SqlCmmandText sql;

        public void SetCommand(SqlCmmandText _sql)
        {
            sql = _sql;
        }

        protected bool ConnectDBReader(Func<SqlDataReader,bool> readAction,string sqlText)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable dt = new DataTable();
            bool returnFlag = false;
            try
            {
                // データベース接続の準備
                connection = new SqlConnection(GetConnectionString());

                // データベースの接続開始
                connection.Open();

                // SQLの実行
                command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = sqlText
                };
                SqlDataReader reader = command.ExecuteReader();
                returnFlag = readAction(reader);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                // データベースの接続終了
                connection.Close();
            }
            return returnFlag;
        }

        protected ArrayList ConnectDBReaderArray(Func<SqlDataReader, ArrayList> readAction, string sqlText)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable dt = new DataTable();
            ArrayList ar = new ArrayList();
            ArrayList data = new ArrayList();
            try
            {
                // データベース接続の準備
                connection = new SqlConnection(GetConnectionString());

                // データベースの接続開始
                connection.Open();

                // SQLの実行
                command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = sqlText
                };
                SqlDataReader reader = command.ExecuteReader();
                ar = readAction(reader);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                // データベースの接続終了
                connection.Close();
            }
            return ar;
        }



        protected bool ConnectDBQuery(Action<SqlCommand> parameterAction, string sqlText)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable dt = new DataTable();
            bool returnFlag = false;
            try
            {
                // データベース接続の準備
                connection = new SqlConnection(GetConnectionString());

                // データベースの接続開始
                connection.Open();

                // SQLの実行
                command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = sqlText
                };
                parameterAction(command);
                //GOは使えない、使えないな... テーブル名、カラム名にGOつけるの禁止にして、スプリットで処理//
                if (command.CommandText.IndexOf("GO") > -1)
                {
                    string[] comm = command.CommandText.Split(new string[] {"GO"}, StringSplitOptions.None);
                    for (int i = 0; i < comm.Length; i++)
                    {
                        if (comm[i] != "")
                        {
                            command.CommandText = comm[i];
                            IAsyncResult result =command.BeginExecuteNonQuery();
                            while(!result.IsCompleted){
                                System.Threading.Thread.Sleep(100);
                                System.Windows.Forms.Application.DoEvents();
                            }
                            command.EndExecuteNonQuery(result);
                        }
                    }
                }
                else
                {
                    IAsyncResult result = command.BeginExecuteNonQuery();
                    while (!result.IsCompleted)
                    {
                        System.Threading.Thread.Sleep(100);
                        System.Windows.Forms.Application.DoEvents();
                    }
                    command.EndExecuteNonQuery(result);
                }

                returnFlag = true;
                //returnFlag = readAction(reader);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                // データベースの接続終了
                connection.Close();
            }
            return returnFlag;
        }





        public string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = GetLocalMachineName(),
                IntegratedSecurity = true,
            };
            return builder.ToString();
        }

        public string GetLocalMachineName()
        {
            return Environment.MachineName;
        }
    }
}
