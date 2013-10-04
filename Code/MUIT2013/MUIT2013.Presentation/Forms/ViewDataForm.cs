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
            var data = dataService.GetViewData(this.tableName).Take(50);
            var k = data.Select(p => (IDictionary<string, object>)p);            
            var dataTable = new DataTable(this.tableName);
            var first = k.First();
            foreach (var key in first.Keys)
	        {
                dataTable.Columns.Add(key);
	        }

            foreach (var item in k)
	        {
                var row = new List<string>();
                foreach (var key in item.Keys)
                {
                    row.Add(item[key].ToString());
                }
                dataTable.Rows.Add(row.ToArray());
	        }
            dgvGridData.DataSource = dataTable;
        }
    }
}
