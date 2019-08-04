using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Configuration;

using ConfigFileLibrary;
using ExcelDataTableLibrary;
using MD5HashLibrary;

namespace Validity_Reminder
{
    public partial class Password : Form
    {
        public bool ReturnValue1 { get; set; }

        readonly ConfigFile XML = new ConfigFile();

        //string hash = ConfigurationManager.AppSettings["password"];
        string hash;

        public Password()
        {
            InitializeComponent();
        }

        void ReturnValues()
        {
            this.ReturnValue1 = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnExit_Click(sender, null);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var md5 = new MD5Hash();
            hash = XML.PasswordHash;

            string source = txtPassword.Text;

            using (MD5 md5Hash = MD5.Create())
            {
                if (md5.VerifyMd5Hash(md5Hash, source, hash))
                {
                    ReturnValues();
                }
                else
                {
                    MessageBox.Show("The hashes are not same.");
                    txtPassword.Text = "";
                }
            }
        }

        private void Password_Load(object sender, EventArgs e)
        {

        }
    }
}
