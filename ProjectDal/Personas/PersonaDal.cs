using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas
{
    public class PersonaDal
    {
        protected static void Insertar(Persona persona)
        {
            string queryString = @"INSERT INTO Persona(IdPersona, Nombre, Apellido, Sexo, idUsuario, Eliminado) 
                                                 VALUES(@IdPersona, @Nombre, @Apellido, @Sexo, @IdUsuario, @Eliminado)";



        }
    }
}
