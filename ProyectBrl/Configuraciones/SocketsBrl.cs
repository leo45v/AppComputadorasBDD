using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Extra;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Configuraciones
{
    public class SocketsBrl
    {
        public bool Insertar(SocketProcesador socketProcesador)
        {
            return SocketsDal.Insert(socketProcesador);
        }
        public List<SocketProcesador> GetAll()
        {
            return SocketsDal.GetAll();
        }
    }
}
