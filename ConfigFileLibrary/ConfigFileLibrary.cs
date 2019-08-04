using System;
using System.Configuration;

namespace ConfigFileLibrary
{
    public class ConfigFile
    {

        string[] sheetNames = { "Sheet1", "Sheet2" };
        string fileName = ConfigurationManager.AppSettings["fileName"];
        string firstNotification = ConfigurationManager.AppSettings["firstNotification"];
        int toExpiration = Convert.ToInt16(ConfigurationManager.AppSettings["toExpiration"]);
        string passwordHash = ConfigurationManager.AppSettings["password"];
        int lastSnoozeIndex = Convert.ToInt16(ConfigurationManager.AppSettings["lastSnoozeIndex"]);
        int[] snoozeValues = Array.ConvertAll(ConfigurationManager.AppSettings["snoozeValues"].Split(';'), int.Parse);
        string[] snoozeTexts = ConfigurationManager.AppSettings["snoozeTexts"].Split(new string[] { ";" }, StringSplitOptions.None);
        int filterIndex = Convert.ToInt16(ConfigurationManager.AppSettings["filterIndex"]);



        private static void SetSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Minimal, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private static void SetSetting(string key, int value)
        {
            SetSetting(key, value.ToString());
        }
        public string[] SheetNames
        {
            get
            {
                return sheetNames;
            }

            set
            {
                sheetNames = value;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                SetSetting("fileName", value);
                fileName = value;
            }
        }

        public string FirstNotification
        {
            get
            {
                return firstNotification;
            }

            set
            {
                firstNotification = value;
            }
        }

        public int ToExpiration
        {
            get
            {
                return toExpiration;
            }

            set
            {
                SetSetting("toExpiration", value);
                toExpiration = value;
            }
        }
        public string PasswordHash
        {
            get
            {
                return passwordHash;
            }

            set
            {
                passwordHash = value;
            }
        }
        public int LastSnoozeIndex
        {
            get
            {
                return lastSnoozeIndex;
            }

            set
            {
                SetSetting("lastSnoozeIndex", value);
                lastSnoozeIndex = value;
            }
        }
        public int[] SnoozeValues
        {
            get
            {
                return snoozeValues;
            }

            set
            {
                snoozeValues = value;
            }
        }
        public string[] SnoozeTexts
        {
            get
            {
                return snoozeTexts;
            }

            set
            {
                snoozeTexts = value;
            }
        }
        public int FilterIndex
        {
            get
            {
                return filterIndex;
            }

            set
            {
                SetSetting("filterIndex", value);
                filterIndex = value;
            }
        }

    }
}
