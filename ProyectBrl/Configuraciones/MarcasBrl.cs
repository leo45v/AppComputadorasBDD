using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Extra;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Configuraciones
{
    public class MarcasBrl
    {
        public bool Insert(Marca marca)
        {
            return MarcasDal.Insert(marca);
        }
        public List<Marca> GetAll()
        {
            return MarcasDal.GetAll();
        }
    }
}
