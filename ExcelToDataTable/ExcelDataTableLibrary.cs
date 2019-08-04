using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace ExcelDataTableLibrary
{
    public class ExcelDataTable
    {
        public enum MathFunction
        {
            Sum,
            Subtract,
            Multiply,
            Divide,
            LessThan,
            GreaterThan,
            Equal
        }

        private string fileName = "_";
        private bool bSheetName = false;
        private string sheetNameRowCaption = "null";

        public void SetFileName(string _fileName)
        {
            fileName = _fileName;
        }
        public void SetSheetNameColumn(bool _state, string _sheetNameRowCaption)
        {
            bSheetName = _state;
            sheetNameRowCaption = _sheetNameRowCaption;
        }

        private readonly bool bCalculationColumn = false;
        readonly string[,] calculationRowCaption = new string[10, 3];
        readonly int[] calculationRowMathFunction = new int[10];
        bool calculationWithTodayDate = false;

        int calculationRowCount = 0;

        public void SetCalculationColumn(string _columnName1, string _columnName2, string _columnNameResult, MathFunction _inputFunction)
        {
            calculationRowCaption[calculationRowCount, 0] = _columnName1;
            calculationRowCaption[calculationRowCount, 1] = _columnName2;
            calculationRowCaption[calculationRowCount, 2] = _columnNameResult;
            calculationRowMathFunction[calculationRowCount] = (int)_inputFunction;

            calculationRowCount++;
        }

        public void SetCalculationColumn(string _columnName1, string _columnNameResult, MathFunction _inputFunction)
        {
            calculationWithTodayDate = true;
            SetCalculationColumn(_columnName1, "null" + calculationRowCount.ToString(), _columnNameResult, _inputFunction);
        }

        public DataTable GetAllDataTables(string _fileName)
        {
            DataTable dtAll = new DataTable();

            string[] sheetNames = GetSheetNames(_fileName);

            foreach (string name in sheetNames)
            {
                using (DataTable dtTemp = GetDataTable(_fileName, name))
                {
                    if (dtTemp != null)
                    {
                        try
                        {
                            dtAll.Merge(dtTemp, false, MissingSchemaAction.Add);

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Please correct all values on sheet : " + name, "Value Type Mismatch Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            return dtAll;
        }
        public DataTable GetAllDataTables()
        {
            return GetAllDataTables(fileName);
        }

        public DataTable GetDataTable(string _fileName, string _sheetName)
        {
            DataTable dataTable = new DataTable();
            DataTable dt = dataTable;
            DataTable tbContainer = dataTable;
            OleDbConnection objConn = null;

            try
            {
                // Connection String. Change the excel file to the file you
                // will search.
                string connString = GetConnectionString(_fileName);

                objConn = new OleDbConnection(connString);

                OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}]", _sheetName), objConn); ;
                oda.Fill(tbContainer);
                oda.Dispose();
                if (calculationWithTodayDate)
                {
                    tbContainer = AddCalculationColumnDate(tbContainer);
                }
                else
                {
                    tbContainer = AddCalculationColumn(tbContainer);
                }
                tbContainer = AddSheetNameColumn(tbContainer, _sheetName); //Add column with sheetName

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

            return tbContainer;
        }
        public DataTable GetDataTable(string _sheetName)
        {
            return GetDataTable(fileName, _sheetName);
        }

        private DataTable AddSheetNameColumn(DataTable _inputDataTable, string _sheetName)
        {
            if (bSheetName)
            {
                DataColumn dc = new DataColumn(sheetNameRowCaption, typeof(string));
                _inputDataTable.Columns.Add(dc);

                //_sheetName = _sheetName.Remove(_sheetName.Length - 2);
                _sheetName = _sheetName.Replace("$", "");
                _sheetName = _sheetName.Replace("'", "");

                foreach (DataRow dr in _inputDataTable.Rows) // search whole table
                {

                    dr[sheetNameRowCaption] = _sheetName; //change the name to sheetName
                }

            }
            return _inputDataTable;
        }

        private DataTable AddCalculationColumn(DataTable _inputDataTable)
        {
            if (!bCalculationColumn)
            {
                for (int i = 0; i < calculationRowCount; i++)
                {
                    DataColumn dc = new DataColumn(calculationRowCaption[i, 2], typeof(int));
                    _inputDataTable.Columns.Add(dc);

                    foreach (DataRow dr in _inputDataTable.Rows) // search whole table
                    {
                        int[] inputNumber = new int[2];

                        if (int.TryParse(dr[calculationRowCaption[i, 0]].ToString(), out inputNumber[0])
                            && int.TryParse(dr[calculationRowCaption[i, 1]].ToString(), out inputNumber[1]))
                        {

                            switch (calculationRowMathFunction[i])
                            {
                                case (int)MathFunction.Sum:
                                    dr[calculationRowCaption[i, 2]] = inputNumber[0] + inputNumber[1];
                                    break;
                                case (int)MathFunction.Subtract:
                                    dr[calculationRowCaption[i, 2]] = inputNumber[0] - inputNumber[1];
                                    break;
                                case (int)MathFunction.Multiply:
                                    dr[calculationRowCaption[i, 2]] = inputNumber[0] * inputNumber[1];
                                    break;
                                case (int)MathFunction.Divide:
                                    dr[calculationRowCaption[i, 2]] = inputNumber[0] / inputNumber[1];
                                    break;
                                default: break;
                            }
                        }
                        else if (DateTime.TryParse(dr[calculationRowCaption[i, 0]].ToString(), out DateTime dt) && DateTime.TryParse(dr[calculationRowCaption[i, 1]].ToString(), out dt))
                        {
                            DateTime[] inputTime = new DateTime[2];
                            inputTime[0] = (DateTime)dr[calculationRowCaption[i, 0]];
                            inputTime[1] = (DateTime)dr[calculationRowCaption[i, 1]];

                            switch (calculationRowMathFunction[i])
                            {
                                case (int)MathFunction.Subtract:
                                    dr[calculationRowCaption[i, 2]] = (int)(inputTime[0] - inputTime[1]).TotalDays;
                                    break;
                                default: break;
                            }

                        }
                    }
                }
            }
            return _inputDataTable;
        }

        private DataTable AddCalculationColumnDate(DataTable _inputDataTable)
        {
            if (!bCalculationColumn)
            {
                for (int i = 0; i < calculationRowCount; i++)
                {
                    DataColumn dc = new DataColumn(calculationRowCaption[i, 2], typeof(int));
                    _inputDataTable.Columns.Add(dc);

                    foreach (DataRow dr in _inputDataTable.Rows) // search whole table
                    {
                        int[] inputNumber = new int[2];

                        if (DateTime.TryParse(dr[calculationRowCaption[i, 0]].ToString(), out DateTime dt))
                        {
                            DateTime[] inputTime = new DateTime[2];
                            inputTime[0] = (DateTime)dr[calculationRowCaption[i, 0]];
                            inputTime[1] = DateTime.Today;

                            switch (calculationRowMathFunction[i])
                            {
                                case (int)MathFunction.Subtract:
                                    dr[calculationRowCaption[i, 2]] = (int)(inputTime[1] - inputTime[0]).TotalDays;
                                    break;
                                default: break;
                            }

                        }
                    }
                }
            }
            return _inputDataTable;
        }


        public string[] GetSheetNames(string _fileName)
        {
            OleDbConnection objConn = null;
            DataTable dt = null;

            try
            {
                // Connection String. Change the excel file to the file you
                // will search.
                string connString = GetConnectionString(_fileName);

                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);

                // Open connection with the database.
                objConn.Open();

                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                string[] excelSheets = new string[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();

                    i++;
                }

                // Loop through all of the sheets if you want too...
                for (int j = 0; j < excelSheets.Length; j++)
                {
                    // Query each excel sheet.
                }

                return excelSheets;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public string[] GetSheetNames()
        {
            return GetSheetNames(fileName);
        }

        private string GetConnectionString(string _fileName)
        {
            string connString;
            try
            {
                // Connection String. Change the excel file to the file you
                // will search.
                FileInfo file = new FileInfo(_fileName);

                if (!file.Exists)
                {
                    throw new Exception("Error, file doesn't exists!");
                }

                string extension = file.Extension;

                switch (extension)
                {
                    case ".xls":
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _fileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        break;
                    case ".xlsx":
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                        break;
                    default:
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _fileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        break;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return connString;
        }
        private string GetConnectionString()
        {
            return GetConnectionString(fileName);
        }

        public DataGridView ChangeRowColorByColumn(DataGridView _input, string _inputColumnName, string _inputColumnValue, Color _inputColor)
        {
            foreach (DataGridViewRow row in _input.Rows)
            {
                if ((string)row.Cells[_inputColumnName].Value == _inputColumnValue)
                {
                    row.DefaultCellStyle.BackColor = _inputColor;
                }
            }
            return _input;
        }

        public DataGridView ChangeCellColor(DataGridView _input, string _inputColumnName, int _inputColumnValue, Color _inputColor, MathFunction _inputFunction)
        {
            try
            {
                foreach (DataGridViewRow row in _input.Rows)
                {

                    if (row.Cells[_inputColumnName].Value != null
                        && int.TryParse(row.Cells[_inputColumnName].Value.ToString(), out int value) == true)
                    {
                        Color newColor = row.Cells[_inputColumnName].Style.BackColor;
                        switch (_inputFunction)
                        {
                            case MathFunction.LessThan:
                                if (value < _inputColumnValue)
                                {
                                    newColor = _inputColor;

                                }
                                break;
                            case MathFunction.GreaterThan:
                                if (value > _inputColumnValue)
                                {
                                    newColor = _inputColor;
                                }
                                break;
                            case MathFunction.Equal:
                                if (value == _inputColumnValue)
                                {
                                    newColor = _inputColor;
                                }
                                break;
                            default: break;
                        }
                        row.Cells[_inputColumnName].Style.BackColor = newColor;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't change cell color");
            }
            return _input;
        }
    }
}