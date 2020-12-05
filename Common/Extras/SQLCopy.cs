using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Extras
{
    public class SQLCopy
    {
        public string SqlQueryCopy { get; set; }
        private readonly string pathDirectory;
        public SQLCopy()
        {
            this.SqlQueryCopy = "";
            this.pathDirectory = @"D:\SQLProducts.sql";
        }
        public SQLCopy(string pathFile)
        {
            this.SqlQueryCopy = "";
            this.pathDirectory = @pathFile;
        }
        public void Clear()
        {
            SqlQueryCopy = "";
        }
        public void WriteIntoFile()
        {
            try
            {
                string filePath = this.pathDirectory;
                using (StreamWriter file = File.AppendText(filePath))
                {
                    file.WriteLine(SqlQueryCopy);
                    file.Close();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
