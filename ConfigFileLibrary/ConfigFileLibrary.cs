using System;
using System.Configuration;

namespace ConfigFileLibrary
{
    public class ConfigFile
    {

        string fileName;
        string firstNotification;
        int toExpiration;
        string passwordHash;
        int lastSnoozeIndex;
        int[] snoozeValues = Array.ConvertAll(ConfigurationManager.AppSettings["snoozeValues"].Split(';'), int.Parse);
        string[] snoozeTexts = ConfigurationManager.AppSettings["snoozeTexts"].Split(new string[] { ";" }, StringSplitOptions.None);
        int filterIndex = Convert.ToInt16(ConfigurationManager.AppSettings["filterIndex"]);
        string sheetCaption = ConfigurationManager.AppSettings["sheetCaption"];
        string[] yellowSheets;
        string[] columnSource = ConfigurationManager.AppSettings["columnSource"].Split(new string[] { ";" }, StringSplitOptions.None);
        string[] columnCalculation = ConfigurationManager.AppSettings["columnCalculation"].Split(new string[] { ";" }, StringSplitOptions.None);

        public void Reload()
        {
            fileName = ConfigurationManager.AppSettings["fileName"];
            firstNotification = ConfigurationManager.AppSettings["firstNotification"];
            toExpiration = Convert.ToInt16(ConfigurationManager.AppSettings["toExpiration"]);
            passwordHash = ConfigurationManager.AppSettings["password"];
            lastSnoozeIndex = Convert.ToInt16(ConfigurationManager.AppSettings["lastSnoozeIndex"]);
            snoozeValues = Array.ConvertAll(ConfigurationManager.AppSettings["snoozeValues"].Split(';'), int.Parse);
            snoozeTexts = ConfigurationManager.AppSettings["snoozeTexts"].Split(new string[] { ";" }, StringSplitOptions.None);
            filterIndex = Convert.ToInt16(ConfigurationManager.AppSettings["filterIndex"]);
            sheetCaption = ConfigurationManager.AppSettings["sheetCaption"];
            columnSource = ConfigurationManager.AppSettings["columnSource"].Split(new string[] { ";" }, StringSplitOptions.None);
            columnCalculation = ConfigurationManager.AppSettings["columnCalculation"].Split(new string[] { ";" }, StringSplitOptions.None);
            yellowSheets = ConfigurationManager.AppSettings["yellowSheets"].Split(new string[] { ";" }, StringSplitOptions.None);
        }
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
        public string SheetCaption
        {
            get
            {
                return sheetCaption;
            }

            set
            {
                sheetCaption = value;
            }
        }
        public string[] YellowSheets
        {
            get
            {
                return yellowSheets;
            }

            set
            {
                yellowSheets = value;
            }
        }

        public string[] ColumnSource
        {
            get
            {
                return columnSource;
            }

            set
            {
                columnSource = value;
            }
        }

        public string[] ColumnCalculation
        {
            get
            {
                return columnCalculation;
            }

            set
            {
                columnCalculation = value;
            }
        }

    }
}
