using MUIT2013.Data.Models;
using MUIT2013.Presentation.Shared;
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
    public partial class ImportForm : FormBase
    {
        public event DataFileUploaded DataFileUploaded;
        public event DataFileActivated DataFileActivated;
        public List<DataFile> DataFileCollection;
        private DataFileViewFactory dfvFactory;
        
        public ImportForm()
        {
            InitializeComponent();
            dgvDataFile.AutoGenerateColumns = false;
            dfvFactory = new DataFileViewFactory();

            // re-load form after data file is uploaded
            DataFileUploaded += ImportForm_Load;

            // re-load form after data file is activated
            DataFileActivated += ImportForm_Load;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "CSV files (*.csv)|*.csv";
            if (DialogResult.OK == dialog.ShowDialog())
            {
                txtFileName.Text = dialog.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName.Text)) return;
            // execute upload data
            var project = projectService.GetCurrentProject();
            dataService.UploadData(txtFileName.Text, project.Name);

            // fires event DataFileUploaded
            if (DataFileUploaded!=null) DataFileUploaded(sender, e);

            // reset controls' value
            txtFileName.Text = "";
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            dgvDataFile.DataSource = this.DataFileCollection = dataFileService.GetList();            
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                var dataFile = (DataFile)dgvDataFile.SelectedRows[0].DataBoundItem;
                var selectedIndex = dgvDataFile.SelectedRows[0].Index;
                dataFileService.Active(dataFile);
                if (DataFileActivated != null) DataFileActivated(dataFile, e);
                // select current row again
                dgvDataFile.Rows[selectedIndex].Selected = true;
                
            }
            catch (Exception ex)
            {
                
            }
        }

        private void dgvDataFile_SelectionChanged(object sender, EventArgs e)
        {
            LoadDataFileProperties();
        }

        private void LoadDataFileProperties()
        {
            if (dgvDataFile.SelectedRows.Count == 0) return;                        
            DataFile selectedDataFile = (DataFile)dgvDataFile.SelectedRows[0].DataBoundItem;
            pgDataFile.SelectedObject = dfvFactory.Create(selectedDataFile);
        }

        private void pgDataFile_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            dgvDataFile.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataFileService.BulkUpdate(this.DataFileCollection);
            MessageBox.Show("Save successful", "Save");
        }

        private void btnViewRawData_Click(object sender, EventArgs e)
        {
            if (dgvDataFile.SelectedRows.Count == 0) return;
            DataFile selectedDataFile = (DataFile)dgvDataFile.SelectedRows[0].DataBoundItem;
            var form = new ViewDataForm(selectedDataFile.RawTableName);
            //form.MdiParent = this;
            form.Show();
        }

        private void btnViewMapData_Click(object sender, EventArgs e)
        {

        }
    }
}
