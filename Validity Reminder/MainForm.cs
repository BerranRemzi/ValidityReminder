using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using ConfigFileLibrary;
using ExcelDataTableLibrary;
using MD5HashLibrary;
using System.Threading;

namespace Validity_Reminder
{
    public partial class MainForm : Form
    {

        int timeoutCounter = 0;
        readonly Notification NotificationForm = new Notification();

        readonly ExcelDataTable Excel = new ExcelDataTable();
        readonly ConfigFile XML = new ConfigFile();
        string fileName;

        private static Mutex _mutex = null;
        bool createdNew;

        public static Mutex Mutex { get => _mutex; set => _mutex = value; }

        public MainForm()
        {
            InitializeComponent();
            EnableDoubleBuffer(dataGridViewExcel);
            XML.Reload();

            for (int i = 0; i < XML.ColumnSource.Length; i++)
            {
                Excel.SetCalculationColumn(XML.ColumnSource[i], XML.ColumnCalculation[i], ExcelDataTable.MathFunction.Subtract);
            }

            LoadExcelFile();

            //timerReminder.Interval = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
            timerReminder.Interval = 10000;
            timeoutCounter = XML.SnoozeValues[XML.LastSnoozeIndex];

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
                            if (int.TryParse(dataGridViewExcel.Rows[i].Cells[XML.ColumnCalculation[j]].Value.ToString(), out int result))
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
            using (Settings SettingsForm = new Settings())
            {
                SettingsForm.ShowDialog();
            }
            timerFirstStart.Start();
        }

        private void DataGridViewExcel_Sorted(object sender, EventArgs e)
        {
            PaintRowsBySheetName();
            PaintCellsInRed();
        }

        private void BtnReminder_Click(object sender, EventArgs e)
        {
            NotificationForm.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!createdNew)
            {
                Application.Exit();
                return;
            }

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
                    using (Settings SettingsForm = new Settings())
                    {
                        SettingsForm.ShowDialog();
                    }
                    break;
                case "Reminder":
                    using (Notification NotificationForm = new Notification())
                    {
                        NotificationForm.ShowDialog();
                    }
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

        private void TimerReminder_Tick(object sender, EventArgs e)
        {
            if (timeoutCounter > 0)
            {
                timeoutCounter--;
            }
            if (timeoutCounter == 0 && (NotificationForm.Visible == false))
            {
                //NotificationForm = new Notification();
                NotificationForm.ShowDialog();
                XML.Reload();
                timeoutCounter = XML.SnoozeValues[XML.LastSnoozeIndex];
            }
        }
        private void TimerFirstStart_Tick(object sender, EventArgs e)
        {
            LoadExcelFile();
            timerFirstStart.Stop();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            const string appName = "Validity Reminder";
            //bool createdNew;
            Mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                Application.Exit();
                return;
            }
            else
            {
                Show();
            }
        }
    }
}
