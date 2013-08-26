using MUIT2013.Data.ViewModels;
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
    public partial class NoRuleForm : Form
    {       
        private IEnumerable<AppliedRuleValue> appliedRuleValues;

        public NoRuleForm(IEnumerable<AppliedRuleValue> appliedRuleValues)
        {
            InitializeComponent();
            dgvNoRuleValues.AutoGenerateColumns = false;
            this.appliedRuleValues = appliedRuleValues;
        }

        private void NoRuleForm_Load(object sender, EventArgs e)
        {
            dgvNoRuleValues.DataSource = appliedRuleValues;
        }

        private void dgvNoRuleValues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
