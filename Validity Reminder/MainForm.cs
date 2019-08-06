using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using ConfigFileLibrary;
using ExcelDataTableLibrary;
using MD5HashLibrary;



namespace Validity_Reminder
{
    public partial class MainForm : Form
    {


        readonly ExcelDataTable Excel = new ExcelDataTable();
        ConfigFile XML = new ConfigFile();
        string fileName;

        public MainForm()
        {
            InitializeComponent();
            EnableDoubleBuffer(dataGridViewExcel);
            XML.Reload();

            for (int i = 0; i < XML.ColumnSource.Length; i++)
            {
                Excel.SetCalculationColumn(XML.ColumnSource[i], XML.ColumnCalculation[i], ExcelDataTable.MathFunction.Subtract);
            }
            foreach( string columnSource in XML.ColumnSource)
            {

                Excel.SetCalculationColumn(columnSource, XML.ColumnCalculation[i], ExcelDataTable.MathFunction.Subtract);
            }
            LoadExcelFile();


        }
        internal static void EnableDoubleBuffer(DataGridView input)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                input,
                new object[] { true });
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            LoadExcelFile();
        }
        void LoadExcelFile()
        {
            XML.Reload();
            fileName = XML.FileName;
            Excel.SetFileName(fileName);
            Excel.SetSheetNameColumn(true, XML.SheetCaption);

            if (fileName != null)
            {
                dataGridViewExcel.DataSource = Excel.GetAllDataTables(fileName);
                PaintRowsBySheetName();
                PaintCellsInRed();
                FillFilterList();
            }
        }

        void PaintCellsInRed()
        {
            try
            {
                for (int i = 0; i < dataGridViewExcel.RowCount - 1; i++)
                {
                    if (XML.ColumnSource.Length == XML.ColumnCalculation.Length)
                    {
                        for (int j = 0; j < XML.ColumnSource.Length; j++)
                        {
                            int result;
                            if (int.TryParse(dataGridViewExcel.Rows[i].Cells[XML.ColumnCalculation[j]].Value.ToString(), out result))
                            {
                                if (result < XML.ToExpiration)
                                {
                                    dataGridViewExcel.Rows[i].Cells[XML.ColumnCalculation[j]].Style.BackColor = Color.Pink;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        public void FillFilterList()
        {
            try
            {
                listColumns.Items.Clear();
                for (int i = 0; i < dataGridViewExcel.Columns.Count; i++)
                {
                    listColumns.Items.Add(dataGridViewExcel.Columns[i].HeaderText);
                }
                if (listColumns.Items.Count > XML.FilterIndex)
                {
                    listColumns.SelectedIndex = XML.FilterIndex;
                }
            }
            catch (Exception) { }

        }

        public void PaintRowsBySheetName()
        {
            try
            {
                for (int i = 0; i < XML.YellowSheets.Length; i++)
                {
                    dataGridViewExcel = Excel.ChangeRowColorByColumn(dataGridViewExcel, XML.SheetCaption, XML.YellowSheets[i], Color.LightYellow);
                }
            }
            catch (Exception) { }

        }
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Settings SettingsForm = new Settings();
            SettingsForm.ShowDialog();
            timerFirstStart.Start();
        }

        private void DataGridViewExcel_Sorted(object sender, EventArgs e)
        {
            PaintRowsBySheetName();
            PaintCellsInRed();
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
            timerFirstStart.Start();
        }

        private void ContextMenuReminder_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Show":
                    this.Show();
                    break;
                case "Settings":
                    Settings SettingsForm = new Settings();
                    SettingsForm.ShowDialog();
                    break;
                case "Reminder":
                    Notification NotificationForm = new Notification();
                    NotificationForm.ShowDialog();
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
            if (txtFilter.Text == "")
            {
                listColumns.Enabled = true;
            }
            else
            {
                listColumns.Enabled = false;
            }
            try
            {
                (dataGridViewExcel.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] Like '*{1}*'", listColumns.Text, txtFilter.Text);

            }
            catch (Exception)
            {

            }
        }

        private void timerReminder_Tick(object sender, EventArgs e)
        {

        }

        private void timerFirstStart_Tick(object sender, EventArgs e)
        {
            LoadExcelFile();
            timerFirstStart.Stop();
        }
    }
}
