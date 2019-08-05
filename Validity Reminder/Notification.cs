using System;
using System.Windows.Forms;
using System.Configuration;

using ConfigFileLibrary;
using ExcelDataTableLibrary;
using MD5HashLibrary;

namespace Validity_Reminder
{
    public partial class Notification : Form
    {
        readonly ExcelDataTable Excel = new ExcelDataTable();
        readonly ConfigFile XML = new ConfigFile();

        public Notification()
        {
            InitializeComponent();
            XML.Reload();

            MainForm.EnableDoubleBuffer(dataGridViewExcel);

            FillSnoozeValues(ref listSnooze, XML.SnoozeTexts);
            FillExpirationDays(ref lblExpiration, XML.ToExpiration);
            listSnooze.SelectedIndex = XML.LastSnoozeIndex;

            Excel.SetCalculationColumn("Техн#Преглед", "ТП Дни", ExcelDataTable.MathFunction.Subtract);
            Excel.SetCalculationColumn("ГО + ЗК", "ГО+ЗК Дни", ExcelDataTable.MathFunction.Subtract);
            Excel.SetCalculationColumn("Тахограф", "Тахограф Дни", ExcelDataTable.MathFunction.Subtract);

            LoadExcelFile();
            HideColumns();
        }
        void HideColumns()
        {
            for (int col = 0; col < dataGridViewExcel.Columns.Count; col++)
            {
                dataGridViewExcel.Columns[col].Visible = false;
            }
            for (int labels = 0; labels < XML.NotificationFilter.Length; labels++)
            {
                dataGridViewExcel.Columns[XML.NotificationFilter[labels]].Visible = true;
            }
            //dataGridViewExcel.Columns[XML.NotificationFilter[1]].Visible = false;
        }

        void LoadExcelFile()
        {
            XML.Reload();
            string fileName = XML.FileName;
            Excel.SetFileName(fileName);
            Excel.SetSheetNameColumn(true, XML.SheetCaption);

            if (fileName != null)
            {
                dataGridViewExcel.DataSource = Excel.GetAllDataTables(fileName);
                try
                {
                    //PaintRowsBySheetName();
                    //PaintCellsInRed();

                }
                catch (Exception)
                {
                }

                try
                {
                    //FillFilterList();
                }
                catch (Exception) { }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void FillSnoozeValues(ref ComboBox inputBox, string[] inputText)
        {
            int lastSnoozeIndex = XML.SnoozeValues.Length;
            inputBox.Items.Clear();
            for (int i = 0; i < lastSnoozeIndex; i++)
            {
                inputBox.Items.Add(inputText[i]);
            }
        }
        void FillExpirationDays(ref Label inputLabel, int inputDays)
        {
            if (inputDays > 1)
            {
                inputLabel.Text = "Expires in " + inputDays.ToString() + " days";
            }
            else
            {
                inputLabel.Text = "Expires in " + inputDays.ToString() + " day";
            }

        }

        private void Notification_Load(object sender, EventArgs e)
        {

        }

        private void ListSnooze_SelectedIndexChanged(object sender, EventArgs e)
        {
            XML.LastSnoozeIndex = listSnooze.SelectedIndex;
        }

        private void TimerReminder_Tick(object sender, EventArgs e)
        {
            //LoadExcelFile();
            HideColumns();
            timerReminder.Stop();
        }
    }
}
