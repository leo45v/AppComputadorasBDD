using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class ClientsBrl
    {
        public static void Insertar(Cliente cliente)
        {
            ClienteDal.Insertar(cliente);
        }
        public static Cliente GetClienteByIdUsuario(Guid idUsuario)
        {
            return ClienteDal.Get_Cliente_By_IdUsuario(idUsuario);
        }
        public static bool Update(Cliente cliente)
        {
            return ClienteDal.Update(cliente);
        }
        public static bool Delete(Guid idCliente)
        {
            return ClienteDal.Delete(idCliente);
        }
    }
}
