using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Extras
{
    public class SQLCopy
    {
        public static string SqlQueryCopy { get; set; }
        public static void Clear()
        {
            SqlQueryCopy = "";
        }
    }
}
