using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Reserva
    {

        public long IdReserva { get; set; } = 0;

        public DateTime FechaReserva { get; set; } = DateTime.Now;
        public Cliente Cliente { get; set; } = new Cliente();
        public ListaProductos Productos { get; set; } = new ListaProductos();
        public bool Recogido { get; set; } = false;
        public bool Eliminado { get; set; } = false;

    }
}
