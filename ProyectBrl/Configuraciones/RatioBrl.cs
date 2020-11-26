using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Pantalla;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Configuraciones
{
    public class RatioBrl
    {
        public bool Insertar(Ratio ratio)
        {
            return RatioDal.Insert(ratio);
        }
        public Ratio Get(byte idRatio)
        {
            return RatioDal.Get(idRatio);
        }
        public List<Ratio> GetAll()
        {
            return RatioDal.GetAll();
        }
    }
}
