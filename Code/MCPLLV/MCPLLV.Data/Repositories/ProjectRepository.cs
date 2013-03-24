using MCPLLV.Data.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private static ISession GetSession() { return SessionProvider.GetSession(); }

        public Project GetById(int id)
        {
            using (var session = GetSession())
            {
                return session.Get<Project>(id);
            }
        }

        public void Add(Project entity)
        {
            using (var session = GetSession())
            {
                session.Save(entity);
            }
        }

        public void Remove(Project entity)
        {
            using (var session = GetSession())
            {
                session.Delete(entity);
            }
        }
    }
}
