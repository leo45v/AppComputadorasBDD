using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class DetalleReserva
    {

        public int Cantidad { get; set; }
        public Reserva Reserva { get; set; }

        public Producto Producto { get; set; }
    }
}
