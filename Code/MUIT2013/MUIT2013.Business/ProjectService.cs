using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class ProjectService: ServiceBase
    {       
        public void CreateProject(string name, string description, string password)
        {
            string projectPath = Path.Combine(DataRepository.GetAbsoluteDataPath(), name);
            string relativePath = Path.Combine("data", name);
            string dbName = name;
            string projectFileName = name + ".db";
            // create project folder if not exists
            if (!Directory.Exists(projectPath)) Directory.CreateDirectory(projectPath);
            // create database file if not exists
            if (!File.Exists(projectFileName)) DataRepository.CreateDatabase(projectFileName, relativePath);
            // connect to project database
            DataRepository.EstablishConnection(projectFileName, relativePath);            
            ProjectRepository.Create(new Data.Models.Project { 
                Description = description,
                Name = name,
                Password = password,
                RelativePath = relativePath
            });            
        }

        public void OpenProject(string name)
        {
            string projectPath = Path.Combine(DataRepository.GetAbsoluteDataPath(), name);
            string relativePath = Path.Combine("data", name);
            string projectFileName = name + ".db";
            // connect to the project database
            DataRepository.EstablishConnection(projectFileName, relativePath);
        }

        public Project GetCurrentProject()
        {
            return ProjectRepository.Get();
        }
    }
}
