﻿using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Fuente : Producto
    {
        public int Potencia { get; set; }
        public ECertificacion Certificacion { get; set; }
    }
}
