using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLinetoDB
{
    public static class SqlTables
    {
        public static string[] TableList = { "player_job","name_index","timeLine_master","timeline" };


        public static string TBL_name_index =
            @"CREATE TABLE [dbo].[name_index](
	        [id] [int] IDENTITY(1,1) NOT NULL,
	        [name] [nvarchar](50) NOT NULL,
	        [memo] [nvarchar](50) NOT NULL
            ) ON [PRIMARY]]";
    }
}
