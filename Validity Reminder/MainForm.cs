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
            Excel.SetCalculationColumn("Техн#Преглед", "ТП Дни", ExcelDataTable.MathFunction.Subtract);
            Excel.SetCalculationColumn("ГО + ЗК", "ГО+ЗК Дни", ExcelDataTable.MathFunction.Subtract);
            Excel.SetCalculationColumn("Тахограф", "Тахограф Дни", ExcelDataTable.MathFunction.Subtract);

            LoadExcelFile();


        }
        void EnableDoubleBuffer(DataGridView input)
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
                try
                {
                    PaintRowsBySheetName();
                    PaintCellsInRed();

                }
                catch (Exception)
                {
                }

                try
                {
                    FillFilterList();
                }
                catch (Exception) { }

            }
        }

        void PaintCellsInRed()
        {
            for (int i = 0; i < dataGridViewExcel.RowCount - 1; i++)
            {
                /*if (int.Parse(dataGridViewExcel.Rows[i].Cells["ТП Дни"].Value.ToString()) < XML.ToExpiration)
                {
                    dataGridViewExcel.Rows[i].Cells["ТП Дни"].Style.BackColor = Color.LightGreen;
                }*/
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

                /*if (int.Parse(dataGridViewExcel.Rows[i].Cells["ГО+ЗК Дни"].Value.ToString()) < XML.ToExpiration)
                {
                    dataGridViewExcel.Rows[i].Cells["ГО+ЗК Дни"].Style.BackColor = Color.Pink;
                }*/

                /*
                if (int.Parse(dataGridViewExcel.Rows[i].Cells["Тахограф Дни"].Value.ToString()) < XML.ToExpiration)
                {
                    dataGridViewExcel.Rows[i].Cells["Тахограф Дни"].Style.BackColor = Color.Orange;
                }*/



            }
        }

        public void FillFilterList()
        {
            for (int i = 0; i < dataGridViewExcel.Columns.Count; i++)
            {
                listColumns.Items.Add(dataGridViewExcel.Columns[i].HeaderText);
            }
            if (listColumns.Items.Count > XML.FilterIndex)
            {
                listColumns.SelectedIndex = XML.FilterIndex;
            }
        }

        public void PaintRowsBySheetName()
        {
            for (int i = 0; i < XML.YellowSheets.Length; i++)
            {
                dataGridViewExcel = Excel.ChangeRowColorByColumn(dataGridViewExcel, XML.SheetCaption, XML.YellowSheets[i], Color.LightYellow);
            }
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
