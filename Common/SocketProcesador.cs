using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class SocketProcesador
    {
        public int IdSocket { get; set; }
        public ESocketProcesador NombreSocket { get; set; }
        public string Descripcion { get; set; }
    }
}
