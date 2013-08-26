using MUIT2013.Business;
using MUIT2013.Presentation.Shared.Events;
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
    public partial class CreateProjectForm : FormBase
    {
        public event ProjectCreated ProjectCreated;
        public CreateProjectForm() : base()
        {
            InitializeComponent();           
        }        

        #region Validation
        private void txtProjectName_Validating(object sender, CancelEventArgs e)
        {
            bool cancel = false;
            if (string.IsNullOrEmpty(txtProjectName.Text))
            {
                cancel = true;
                this.errorProvider.SetError(txtProjectName, "Please enter Project Name");
            }
            e.Cancel = cancel;
        }        

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            bool cancel = false;
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                cancel = true;
                this.errorProvider.SetError(txtPassword, "Please enter Password");
            }
            else if (txtPassword.Text != txtConfirmationPassword.Text)
            {
                cancel = true;
                this.errorProvider.SetError(txtPassword, "Password and Confirmation Password do not match together");
            }
            e.Cancel = cancel;
        }

        private void txtConfirmationPassword_Validating(object sender, CancelEventArgs e)
        {
            bool cancel = false;
            if (string.IsNullOrEmpty(txtConfirmationPassword.Text))
            {
                cancel = true;
                this.errorProvider.SetError(txtConfirmationPassword, "Please enter Password");
            }
            else if (txtPassword.Text != txtConfirmationPassword.Text)
            {
                cancel = true;
                this.errorProvider.SetError(txtConfirmationPassword, "Password and Confirmation Password do not match together");
            }
            e.Cancel = cancel;
        }

        private void txtProjectName_Validated(object sender, EventArgs e)
        {
            this.errorProvider.SetError(txtProjectName, string.Empty);
        }

        private void txtPassword_Validated(object sender, EventArgs e)
        {
            this.errorProvider.SetError(txtPassword, string.Empty);
        }

        private void txtConfirmationPassword_Validated(object sender, EventArgs e)
        {
            this.errorProvider.SetError(txtConfirmationPassword, string.Empty);
        }
        #endregion

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled)) return;
            projectService.CreateProject(txtProjectName.Text, txtProjectDescription.Text, txtPassword.Text);
            if (ProjectCreated != null) ProjectCreated(projectService, e);    
            this.Close();            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateProjectForm_Load(object sender, EventArgs e)
        {

        }
    }
}
