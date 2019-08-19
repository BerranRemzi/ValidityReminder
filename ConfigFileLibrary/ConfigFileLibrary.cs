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
        int[] snoozeValues;
        string[] snoozeTexts;
        int filterIndex;
        string sheetCaption;
        string[] yellowSheets;
        string[] columnSource;
        string[] columnCalculation;
        string[] notificationFilter;
        int[] mainWindowSize;
        int[] notificationWindowSize;
        string ignoreCheckColumn;
        string ignoreText;

        public void Reload()
        {
            fileName = ReadString("fileName");
            firstNotification = ReadString("firstNotification");
            toExpiration = ReadInt("toExpiration");
            passwordHash = ReadString("password");
            lastSnoozeIndex = ReadInt("lastSnoozeIndex");
            snoozeValues = ReadIntArray("snoozeValues", ";");
            snoozeTexts = ReadStringArray("snoozeTexts", ";");
            filterIndex = ReadInt("filterIndex");
            sheetCaption = ReadString("sheetCaption");
            columnSource = ReadStringArray("columnSource", ";");
            columnCalculation = ReadStringArray("columnCalculation", ";");
            yellowSheets = ReadStringArray("yellowSheets", ";");
            notificationFilter = ReadStringArray("notificationFilter", ";");
            mainWindowSize = ReadIntArray("mainWindowSize", ";");
            notificationWindowSize = ReadIntArray("notificationWindowSize", ";");

            ignoreCheckColumn = ReadString("ignoreCheckColumn");
            ignoreText = ReadString("ignoreText");
        }
        public string ReadString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string[] ReadStringArray(string key, string delimiter)
        {
            return ConfigurationManager.AppSettings[key].Split(new string[] { delimiter }, StringSplitOptions.None);
        }

        public int ReadInt(string key)
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings[key]);
        }
        public int[] ReadIntArray(string key, string delimiter)
        {
            return Array.ConvertAll(ConfigurationManager.AppSettings[key].Split(';'), int.Parse);
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
        public string[] NotificationFilter
        {
            get
            {
                return notificationFilter;
            }

            set
            {
                notificationFilter = value;
            }
        }
        public int[] MainWindowSize
        {
            get
            {
                return mainWindowSize;
            }

            set
            {
                mainWindowSize = value;
            }
        }
        public int[] NotificationWindowSize
        {
            get
            {
                return notificationWindowSize;
            }

            set
            {
                notificationWindowSize = value;
            }
        }


        public string IgnoreText
        {
            get
            {
                return ignoreText;
            }

            set
            {
                ignoreText = value;
            }
        }

        public string IgnoreCheckColumn
        {
            get
            {
                return ignoreCheckColumn;
            }

            set
            {
                ignoreCheckColumn = value;
            }
        }
    }
}
