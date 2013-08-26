using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data
{
    public class Migrator
    {
        string connectionString;

        public Migrator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }


            public string ProviderSwitches
            {
                get { throw new NotImplementedException(); }
            }
        }

        public void Migrate(Action<IMigrationRunner> runnerAction)
        {
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 0 };
            var factory = new FluentMigrator.Runner.Processors.Sqlite.SqliteProcessorFactory();
            var assembly = Assembly.GetExecutingAssembly();

            //using (var announcer = new NullAnnouncer())
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var migrationContext = new RunnerContext(announcer)
            {
                #if DEBUG
                    // will create testdata
                    Profile = "development"
                #endif
            };
            var processor = factory.Create(this.connectionString, announcer, options);
            var runner = new MigrationRunner(assembly, migrationContext, processor);
            runnerAction(runner);
        }
    }
}
