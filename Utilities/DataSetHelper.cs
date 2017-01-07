using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Data;


namespace Utilities
{
    public class DataSetHelper
    {
        /// <summary>
        /// Get DataSet Excel File like .csv, .xls, .xlsx 
        /// The excel's sheet name the same as file name that we want to import
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="isFirstRowHeader"></param>
        /// <require></require>
        public static DataSet GetDataSetFromExcelFile(string fileName, bool isFirstRowHeader)
        {
            DataSet ds = null;
            try
            {                
                string header = isFirstRowHeader ? "Yes" : "No";
                OleDbConnection conn = null;
                OleDbDataAdapter adapter = null;

                if (Path.GetExtension(fileName) == ".csv" || Path.GetExtension(fileName) == ".txt")
                {
                    ds = GetDataSetFromCSV(fileName, "\t");
                    /*conn = new OleDbConnection
                           ("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " +
                             Path.GetDirectoryName(fileName) +
                             "; Extended Properties = \"Text;HDR=" + header + ";FMT=TabDelimited;\"");
                    conn.Open();
                    adapter = new OleDbDataAdapter("SELECT * FROM " + Path.GetFileName(fileName), conn);*/
                }
                else if (Path.GetExtension(fileName) == ".xls")
                {
                    conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=" + header + ";IMEX=2\"");
                    conn.Open();
                    var sheetName = "[" + Path.GetFileNameWithoutExtension(fileName) + "]";
                    adapter = new OleDbDataAdapter("SELECT * FROM " + sheetName, conn);
                }
                else if (Path.GetExtension(fileName) == ".xlsx")
                {
                    conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=" + header + ";IMEX=1;';");
                    conn.Open();
                    var sheetName = "[" + Path.GetFileNameWithoutExtension(fileName) + "]";
                    adapter = new OleDbDataAdapter("SELECT * FROM " + sheetName, conn);
                }

                if (ds == null)
                {
                    ds = new DataSet("Temp");
                    adapter.Fill(ds);
                    adapter.Dispose();
                    conn.Dispose();
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return ds;
        }

        private static DataSet GetDataSetFromCSV(string filePath,string delimiter)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();            
            using (StreamReader sr = new StreamReader(filePath))
            {                
                string rowHeader = sr.ReadLine();
                string[] headers = rowHeader.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries); //Or ","
                foreach (string header in headers)
                {
                    DataColumn dc = new DataColumn(header);
                    dt.Columns.Add(dc);
                }                

                while (!sr.EndOfStream)
                {
                    string dataLine = sr.ReadLine();
                    string[] datas = dataLine.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries); //Or ","
                    DataRow row = dt.NewRow();
                    int colIndex = 0;
                    foreach (string data in datas)
                    {
                        row[colIndex] = data;
                        colIndex++;
                    }
                    dt.Rows.Add(row);
                }                
            }
            ds.Tables.Add(dt);
            return ds;
        }
    }
}
