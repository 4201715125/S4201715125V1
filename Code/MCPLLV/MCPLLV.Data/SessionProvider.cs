using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MCPLLV.Data.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data
{    
    public class SessionProvider
    {
        private static string connectionString = @"Data Source=localhost;Initial Catalog=Anodisys;Integrated Security=True;Pooling=False";
        private static FluentConfiguration configuration;
        private static ISessionFactory sessionFactory;

        public static FluentConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = Fluently.Configure()
                        .Database(MsSqlConfiguration
                            .MsSql2008.ConnectionString(
                            connectionString
                            )
                            .ShowSql()
                        )
                        .Mappings(m => m.FluentMappings
                            .AddFromAssemblyOf<UserMap>()
                        )
                        .ExposeConfiguration(config => new SchemaExport(config).Create(true, true));
                }
                return configuration;
            }
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                    sessionFactory = Configuration.BuildSessionFactory();
                return sessionFactory;
            }
        }

        private SessionProvider()
        { }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
