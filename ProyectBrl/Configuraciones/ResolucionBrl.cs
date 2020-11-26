using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Pantalla;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Configuraciones
{
    public class ResolucionBrl
    {
        public bool Insertar(Resolucion resolucion)
        {
            return ResolucionDal.Insert(resolucion);
        }
        public Resolucion Get(byte idResolucion)
        {
            return ResolucionDal.Get(idResolucion);
        }
        public List<Resolucion> GetAll()
        {
            return ResolucionDal.GetAll();
        }
    }
}
