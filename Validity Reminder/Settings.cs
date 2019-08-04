using System;
using System.Windows.Forms;
using ConfigFileLibrary;
using ExcelDataTableLibrary;
using MD5HashLibrary;

namespace Validity_Reminder
{
    public partial class Settings : Form
    {

        //readonly ExcelDataTable Excel = new ExcelDataTable();
        readonly ConfigFile XML = new ConfigFile();

        public Settings()
        {
            InitializeComponent();
            Init();
        }

        private void button2_Click(object sender, EventArgs e)
        {
       
        }
        private void Init()
        {
            txtFileName.Text = XML.FileName;
            txtDays.Text = XML.ToExpiration.ToString();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Excel File",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xls",
                Filter = "XLS files (*.xls)|*.xls",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
            }
        }

        private void BtnSaveAndExit_Click(object sender, EventArgs e)
        {
            XML.ToExpiration = int.Parse(txtDays.Text);
            XML.FileName = txtFileName.Text;
        }
    }
}
