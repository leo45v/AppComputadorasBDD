using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Reservas;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Reservas
{
    public class ReservasBrl
    {
        public static bool Insert(Reserva reserva)
        {
            return ReservasDal.Insert(reserva);
        }
        public static Reserva Get(long idReserva)
        {
            return ReservasDal.Get(idReserva);
        }
        public static bool Delete(long idReserva)
        {
            return ReservasDal.Delete(idReserva);
        }
        public static bool Habilitar(long idReserva)
        {
            return ReservasDal.Habilitar(idReserva);
        }
        public static bool Regocer(long idReserva)
        {
            return ReservasDal.Recoger(idReserva);
        }
        public static bool CancelarRecoger(long idReserva)
        {
            return ReservasDal.NoRecogido(idReserva);
        }
        public static List<Reserva> GetReservas(Guid idCliente)
        {
            return ReservasDal.GetReservas(idCliente);
        }
        public static bool QuitarProducto(Guid idProducto, long idReserva)
        {
            return ReservasDal.QuitarProducto_Reserva(idProducto, idReserva);
        }
    }
}
