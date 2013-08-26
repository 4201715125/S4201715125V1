using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using MUIT2013.Data.Models;

namespace MUIT2013.Data.Repository
{
    public class ProjectRepository: RepositoryBase
    {        
        public static long Create(Project model)
        {
            long id = 0;
            using (var con = EstablishConnection())
            {
                con.Open();
                con.Execute(@"
                    INSERT INTO PROJECTS(name, description, password, relativePath)
                    VALUES(@Name,@Description,@Password,@RelativePath)
                ", model);
                id = con.Query<long>("SELECT last_insert_rowid();").First();
                model.Id = id;                               
                con.Close();
            }
            return id;
        }

        public static IEnumerable<Project> GetList()
        {
            IEnumerable<Project> projects;
            using (var con = EstablishConnection())
            {
                con.Open();
                projects = con.GetList<Project>();
                con.Close();
            }
            return projects;
        }

        public static Project Get()
        {
            Project project;
            using (var con = EstablishConnection())
            {
                con.Open();
                project = con.GetList<Project>().FirstOrDefault();
                con.Close();
            }
            return project;
        }
    }
}
