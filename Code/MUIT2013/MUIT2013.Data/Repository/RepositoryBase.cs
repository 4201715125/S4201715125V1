using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Repository
{
    public class RepositoryBase
    {
        private static string dbFilePath;

        public static string ConnectionString(string dbFilePath)
        {
            return string.Format("Data Source={0}", dbFilePath);
        }

        public static DbConnection EstablishConnection()
        {            
            return new SQLiteConnection(ConnectionString(dbFilePath));
        }

        public static DbConnection EstablishConnection(string dbName, string relativePath)
        {
            dbFilePath = GetAbsoluteDatabaseFilePath(dbName, relativePath);
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);
            return EstablishConnection();
        }

        public static void Setup()
        {
            string dataPath = GetAbsoluteDataPath();
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
        }

        public static string GetAbsoluteDataPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "data");
        }

        public static string GetRelativeDataPath()
        {
            return "data";
        }

        public static string GetAbsoluteProjectPath(string projectName)
        {
            return Path.Combine(GetAbsoluteDataPath(), projectName);
        }

        public static string GetRelativeProjectPath(string projectName)
        {
            return Path.Combine(GetRelativeDataPath(), projectName);
        }

        public static string GetAbsoluteDatabaseFilePath(string dbName, string relativePath)
        {
            return Path.Combine(Environment.CurrentDirectory, relativePath, dbName);
        }

        public static void CreateDatabase(string dbName, string relativePath)
        {
            string dbFilePath = GetAbsoluteDatabaseFilePath(dbName, relativePath);
            string connectionString = ConnectionString(dbFilePath);
            Migrator migrator = new Migrator(connectionString);
            #if DEBUG
            migrator.Migrate(runner => runner.MigrateDown(0));
            #endif
            migrator.Migrate(runner => runner.MigrateUp());
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);
        }


    }
}
