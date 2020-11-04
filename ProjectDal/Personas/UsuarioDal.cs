using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas
{
    public class UsuarioDal
    {

        protected static void Insertar(Usuario usuario)
        {
            string queryString = @"INSERT INTO Usuario(IdUsuario, NombreUsuario, Contrasenia, Eliminado, IdRol) 
                                                VALUES(@IdUsuario, @NombreUsuario, @Contrasenia, @Eliminado, @IdRol)";
            try
            {
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdUsuario", usuario.IdUsuario);
                OperationsSql.AddWithValueString("NombreUsuario", usuario.NombreUsuario);
                OperationsSql.AddWithValueString("Contrasenia", usuario.Contrasenia);
                OperationsSql.AddWithValueString("Eliminado", usuario.Eliminado);
                OperationsSql.AddWithValueString("IdRol", usuario.Rol.IdRol);
                OperationsSql.ExecuteBasicCommandWithTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
