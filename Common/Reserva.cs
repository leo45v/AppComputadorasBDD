using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Reserva
    {

        public int IdReserva { get; set; }

        public DateTime FechaReserva { get; set; }
        public Persona Persona { get; set; }


    }
}
