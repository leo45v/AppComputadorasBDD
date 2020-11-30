using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Reserva
    {

        public long IdReserva { get; set; } = 0;

        public DateTime FechaReserva { get; set; } = DateTime.Now;
        public Cliente Cliente { get; set; } = new Cliente();
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public bool Recogido { get; set; } = false;
        public bool Eliminado { get; set; } = false;

    }
}
