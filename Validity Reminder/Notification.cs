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

            FillSnoozeValues(ref listSnooze, XML.SnoozeTexts);
            FillExpirationDays(ref lblExpiration, XML.ToExpiration);
            listSnooze.SelectedIndex = XML.LastSnoozeIndex;
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
                inputLabel.Text = "Expires in " + inputDays.ToString() +" days";
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
    }
}
