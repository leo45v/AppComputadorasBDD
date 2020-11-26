using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Colores
    {
        public short IdColor { get; set; }
        public string Nombre { get; set; }
        public Color ColorRGB { get; set; }
        public string Color_ToString
        {
            get
            {
                return ColorRGB.R + "," + ColorRGB.G + "," + ColorRGB.B;
            }
        }
    }
}
