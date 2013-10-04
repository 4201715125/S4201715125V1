using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MUIT2013.Data.Models;
using MUIT2013.Presentation.Shared;
using MUIT2013.Presentation.Shared.Events;

namespace MUIT2013.Presentation.Forms
{
    public partial class HandlerTrackerForm : FormBase
    {
        public event SelectHandlerTracker SelectHandlerTracker;
        public HandlerTrackerForm()
        {
            InitializeComponent();
            dgvHandlerTracker.AutoGenerateColumns = false;
        }

        private void HandlerTrackerForm_Load(object sender, EventArgs e)
        {
            dgvHandlerTracker.DataSource = handerTrackerService.GetList();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (dgvHandlerTracker.SelectedRows.Count == 0) return;
            HandlerTracker handlerTracker = (HandlerTracker)dgvHandlerTracker.SelectedRows[0].DataBoundItem;
            if (SelectHandlerTracker!=null)
            {
                SelectHandlerTracker(handlerTracker, e);
            }
        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            if (dgvHandlerTracker.SelectedRows.Count == 0) return;
            HandlerTracker handlerTracker = (HandlerTracker)dgvHandlerTracker.SelectedRows[0].DataBoundItem;
            var form = new ViewDataForm(handlerTracker.TableName);
            //form.MdiParent = this;
            form.Show();
        }
    }
}
