using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla
{
    public class Resolucion
    {
        public byte IdResolucion { get; set; }
        public string NombreResolucion { get; set; }
        public static Resolucion Dictionary_To_Resolucion(Dictionary<string, object> data)
        {
            return new Resolucion()
            {
                IdResolucion = (byte)data["IdResolucion"],
                NombreResolucion = (string)data["NombreResolucion"]
            };
        }
    }
}
