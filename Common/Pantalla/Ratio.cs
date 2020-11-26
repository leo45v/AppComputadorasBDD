using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla
{
    public class Ratio
    {
        public byte IdRatio { get; set; }
        public string NombreRatio { get; set; }
        public static Ratio Dictionary_To_Ratio(Dictionary<string, object> data)
        {
            return new Ratio()
            {
                IdRatio = (byte)data["IdRatio"],
                NombreRatio = (string)data["NombreRatio"]
            };
        }
    }
}
