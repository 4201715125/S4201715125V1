using MUIT2013.Business;
using MUIT2013.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUIT2013.Presentation.Forms
{
    public partial class FormBase : Form
    {
        protected ProjectService projectService;
        protected DataService dataService;
        protected DataFileService dataFileService;
        protected ColumnDefinitionService columnDefinitionService;
        protected static DataFile ActivateDataFile;
        public FormBase()
        {            
            projectService = new ProjectService();
            dataService = new DataService();
            dataFileService = new DataFileService();
            columnDefinitionService = new ColumnDefinitionService();
        }
    }
}
