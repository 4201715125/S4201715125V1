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
    public partial class ViewDataForm : FormBase
    {
        private string tableName;
        public ViewDataForm(string tableName)
        {
            InitializeComponent();
            this.tableName = tableName;
            dgvGridData.AutoGenerateColumns = true;
        }

        private void ViewDataForm_Load(object sender, EventArgs e)
        {
            var dataTable = dataService.GetViewData(this.tableName);
            dgvGridData.DataSource = dataTable;
        }
    }
}
