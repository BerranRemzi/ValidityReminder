using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using ConfigFileLibrary;
using ExcelDataTableLibrary;
using MD5HashLibrary;

namespace Validity_Reminder
{
    public partial class MainForm : Form
    {
        readonly ExcelDataTable Excel = new ExcelDataTable();
        readonly ConfigFile XML = new ConfigFile();
        string fileName;

        public MainForm()
        {
            InitializeComponent();
            LoadExcelFile();
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            LoadExcelFile();
        }
        private void LoadExcelFile()
        {
            fileName = XML.FileName;
            Excel.SetFileName(fileName);
            Excel.SetSheetNameColumn(true, "Sheet Caption");
            if (fileName != null)
            {
                dataGridViewExcel.DataSource = Excel.GetAllDataTables();
                PaintRows();
                FillFilterList();
            }
        }
        public void FillFilterList()
        {
            for (int i = 0; i < dataGridViewExcel.Columns.Count; i++)
            {
                listColumns.Items.Add(dataGridViewExcel.Columns[i].HeaderText);
            }
            listColumns.SelectedIndex = XML.FilterIndex;
        }

        public void PaintRows()
        {
            dataGridViewExcel = Excel.ChangeRowColorByColumn(dataGridViewExcel, "Sheet Caption", "ЕТ влекачи", Color.LightYellow);
            dataGridViewExcel = Excel.ChangeRowColorByColumn(dataGridViewExcel, "Sheet Caption", "ЕТ ремаркета", Color.LightYellow);
        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Settings SettingsForm = new Settings();
            SettingsForm.ShowDialog();
        }

        private void DataGridViewExcel_Sorted(object sender, EventArgs e)
        {
            PaintRows();
        }

        private void BtnReminder_Click(object sender, EventArgs e)
        {
            Notification NotificationForm = new Notification();
            NotificationForm.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void NotifyIconReminder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void ContextMenuReminder_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Open":
                    this.Show();
                    break;
                case "Settings":
                    Settings SettingsForm = new Settings();
                    SettingsForm.ShowDialog();
                    break;
                case "Exit":
                    if (CheckPassword() == true)
                    {
                        Dispose(true);
                    }
                    break;
                default: break;
            }
        }
        bool CheckPassword()
        {
            bool isPasswordCorrect = false;
            using (var form = new Password())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    isPasswordCorrect = form.ReturnValue1;
                }
            }

            return isPasswordCorrect;
        }

        private void ListColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            XML.FilterIndex = listColumns.SelectedIndex;
        }

        private void TxtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewExcel.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] Like '*{1}*'", listColumns.Text, txtFilter.Text);

            }
            catch (Exception)
            {

            }
        }
    }
}
