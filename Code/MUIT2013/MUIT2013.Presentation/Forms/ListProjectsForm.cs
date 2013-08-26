using MUIT2013.Business;
using MUIT2013.Data.Repository;
using MUIT2013.Presentation.Shared.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUIT2013.Presentation.Forms
{
    public partial class ListProjectsForm : Form
    {
        private ProjectService projectService;
        public event ProjectOpened ProjectOpened;
        public ListProjectsForm()
        {
            InitializeComponent();
            projectService = new ProjectService();
        }

        #region Events
        private void btnOpen_Click(object sender, EventArgs e)
        {
            var projectName = (string)lsbProject.SelectedItem;
            projectService.OpenProject(projectName);
            if (ProjectOpened != null) ProjectOpened(sender, e);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListProjectsForm_Load(object sender, EventArgs e)
        {            
            LoadProjects();            
        }
        #endregion

        #region Initialize data
        private void LoadProjects()
        {
            IEnumerable<string> projectDirectories = Directory.EnumerateDirectories(ProjectRepository.GetAbsoluteDataPath());
            foreach (var projectDirectoryPath in projectDirectories)
            {
                var directoryInfo = new DirectoryInfo(projectDirectoryPath);
                lsbProject.Items.Add(directoryInfo.Name);
            }
        }
        #endregion        
    }
}
