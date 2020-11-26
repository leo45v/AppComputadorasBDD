using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Extra;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Configuraciones
{
    public class ColoresBrl
    {
        public bool Insert(Colores colores)
        {
            return ColoresDal.Insert(colores);
        }
        public List<Colores> GetAll()
        {
            return ColoresDal.GetAll();
        }

    }
}
