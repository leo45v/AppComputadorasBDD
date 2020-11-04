using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class RamBrl : List<Ram>
    {
     
    
        public new void Add(Ram ram)
        {
            if (RamDal.Insertar(ram))
            {
                base.Add(ram);
            }
        }
        public new void Remove(Ram ram)
        {
            if (RamDal.Delete_Ram(ram.IdProducto))
            {
                base.Remove(ram);
            }
        }


    }
}
