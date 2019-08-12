using System;
using System.Windows.Forms;
using System.Configuration;

using ConfigFileLibrary;
using ExcelDataTableLibrary;
using MD5HashLibrary;
using System.Drawing;

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
            try
            {
                for (int col = 0; col < dataGridViewExcel.Columns.Count; col++)
                {
                    dataGridViewExcel.Columns[col].Visible = false;
                }
                for (int labels = 0; labels < XML.NotificationFilter.Length; labels++)
                {
                    dataGridViewExcel.Columns[XML.NotificationFilter[labels]].Visible = true;
                }
            }
            catch (Exception) {

            }
        }
        void HideRows()
        {
            int rowCount = dataGridViewExcel.Rows.Count - 1;
            for (int row = 0; row < rowCount; row++)
            {
                bool visible = false;
                for (int i = 0; i < Excel.calculationRowCount; i++)
                {
                    int result;
                    if (int.TryParse(dataGridViewExcel.Rows[row].Cells[XML.ColumnCalculation[i]].Value.ToString(), out result))
                    {
                        if (result < XML.ToExpiration)
                        {
                            //dataGridViewExcel.Rows[i].Cells[XML.ColumnCalculation[j]].Style.BackColor = Color.Pink;
                            visible = true;
                        }
                    }
                }
                dataGridViewExcel.CurrentCell = null;
                dataGridViewExcel.Rows[row].Visible = visible;
            }
        }

        void LoadExcelFile()
        {
            string fileName = XML.FileName;
            Excel.SetFileName(fileName);
            Excel.SetSheetNameColumn(true, XML.SheetCaption);

            if (fileName != null)
            {
                dataGridViewExcel.DataSource = Excel.GetAllDataTables(fileName);

                PaintRowsBySheetName();
                PaintCellsInRed();
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

        public void PaintRowsBySheetName()
        {
            try
            {
                for (int i = 0; i < XML.YellowSheets.Length; i++)
                {
                    dataGridViewExcel = Excel.ChangeRowColorByColumn(dataGridViewExcel, XML.SheetCaption, XML.YellowSheets[i], Color.LightYellow);
                }
            }
            catch (Exception)
            {
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
            timerReminder.Start();
        }

        private void ListSnooze_SelectedIndexChanged(object sender, EventArgs e)
        {
            XML.LastSnoozeIndex = listSnooze.SelectedIndex;
        }

        private void TimerReminder_Tick(object sender, EventArgs e)
        {
            //LoadExcelFile();
            PaintRowsBySheetName();
            PaintCellsInRed();
            HideColumns();
            HideRows();
            timerReminder.Stop();
        }

        private void dataGridViewExcel_Sorted(object sender, EventArgs e)
        {
            PaintRowsBySheetName();
            PaintCellsInRed();
            HideRows();
        }
    }
}
