using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace DataAccess
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return PathProvider.TryGetSolutionDirectoryInfo() + "\\Amsterdam_csharp_task.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }
}
