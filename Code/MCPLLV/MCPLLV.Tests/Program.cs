using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MCPLLV.Data.Mappings;
using MCPLLV.Data.Models;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Tests
{
    class Program
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\Projects\anodisys.mdf;Integrated Security=True;User Instance=True";
        static void Main(string[] args)
        {
            var sessionFactory = CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userGroup = new UserGroup { 
                        Name = "Staff",
                        Description = "",
                        Priority = 0
                    };

                    userGroup.Users = new List<User> { 
                        new User{
                            UserName = "user1",
                            Password = "123456"
                        }
                    };

                    session.SaveOrUpdate(userGroup);

                    transaction.Commit();
                }
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration
                    .MsSql2008.ConnectionString(connectionString)
                )
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<UserMap>()
                )
                .ExposeConfiguration(config => new SchemaExport(config).Create(true, true))
                .BuildSessionFactory();
        }
    }
}
