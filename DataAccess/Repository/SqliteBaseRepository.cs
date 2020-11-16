using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace DataAccess
{
    public class SqLiteBaseRepository
    {
        public readonly IConfiguration _configuration;
        public SqLiteBaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SQLiteConnection DbConnection()
        {
            var connectionString = ConnectionString();
            var builder = new SQLiteConnectionStringBuilder(connectionString);
            builder.DataSource = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, builder.DataSource));
            connectionString = builder.ToString();
            return new SQLiteConnection(connectionString);
        }

        public string ConnectionString(string id = "SqliteLogex")
        {
            return _configuration.GetConnectionString(id);
        }
    }
}
