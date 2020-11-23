using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Extras
{
    public class SQLCopy
    {
        public static string SqlQueryCopy { get; set; }
        public static void Clear()
        {
            SqlQueryCopy = "";
        }
        public static void WriteIntoFile()
        {
            string filePath = @"D:\SQLProducts.sql";
            using (StreamWriter file = File.AppendText(filePath))
            {
                file.WriteLine(SqlQueryCopy);
                file.Close();
            }
        }
    }
}
