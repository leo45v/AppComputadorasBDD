using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas
{
    public class PersonaDal
    {
        public static bool cascada = false;
        public static bool Insertar(Persona persona)
        {
            bool estado = false;
            string queryString = @"INSERT INTO Persona(IdPersona, Nombre, Apellido, Sexo, idUsuario, Eliminado, FechaNacimiento) 
                                                VALUES(@IdPersona, @Nombre, @Apellido, @Sexo, @IdUsuario, @Eliminado, @FechaNacimiento)";
            try
            {
                OperationsSql.OpenConnection();
                UsuarioDal.cascada = true;
                if (UsuarioDal.Insertar(persona.Usuario as Usuario))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString("IdPersona", persona.IdPersona);
                    OperationsSql.AddWithValueString("Nombre", persona.Nombre);
                    OperationsSql.AddWithValueString("Apellido", persona.Apellido);
                    OperationsSql.AddWithValueString("Sexo", persona.Sexo);
                    OperationsSql.AddWithValueString("Eliminado", persona.Eliminado);
                    OperationsSql.AddWithValueString("FechaNacimiento", persona.FechaNacimiento);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    if (!cascada)
                    { OperationsSql.ExecuteTransactionCommit(); }
                    estado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { UsuarioDal.cascada = false; if (!cascada) { OperationsSql.CloseConnection(); } }
            return estado;
        }
        public static Persona Obtener(Persona persona)
        {
            return Obtener_By_Id(persona.IdPersona);
        }
        public static Persona Obtener_By_Id(Guid idPersona)
        {
            Persona persona = null;
            string queryString = @"SELECT pe.IdPersona, pe.Nombre, pe.Apellido, pe.Sexo, pe.idUsuario as IdUsuario, pe.Eliminado, 
                                   usr.NombreUsuario, usr.Contrasenia, usr.Eliminado as Deleted, usr.IdRol, 
                                   Rol.NombreRol  
                                   FROM Persona pe 
                                   INNER JOIN Usuario usr ON usr.IdUsuario = pe.idUsuario 
                                   INNER JOIN Rol ON Rol.IdROl = usr.IdRol 
                                   WHERE IdPersona = @IdPersona";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdPersona", idPersona);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    persona = ObjectData_To_Persona(data);
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { OperationsSql.CloseConnection(); }
            return persona;
        }
        public static List<Persona> Obtener_Lista_Personas()
        {
            List<Persona> personas = null;
            string queryString = @"SELECT pe.IdPersona, pe.Nombre, pe.Apellido, pe.Sexo, pe.idUsuario as IdUsuario, pe.Eliminado, 
                                   usr.NombreUsuario, usr.Contrasenia, usr.Eliminado as Deleted, usr.IdRol, 
                                   Rol.NombreRol  
                                   FROM Persona pe 
                                   INNER JOIN Usuario usr ON usr.IdUsuario = pe.idUsuario 
                                   INNER JOIN Rol ON Rol.IdROl = usr.IdRol";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    personas = new List<Persona>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        personas.Add(ObjectData_To_Persona(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { OperationsSql.CloseConnection(); }
            return personas;
        }
        public static bool Update(Persona persona)
        {
            bool estado = false;
            string queryString = @"UPDATE Persona 
                                   SET Nombre = @Nombre, 
                                       Apellido = @Apellido, 
                                       Sexo = @Sexo, 
                                       Eliminado = @Eliminado
                                   WHERE IdPersona = @IdPersona";
            try
            {
                OperationsSql.AddWithValueString("IdPersona", persona.IdPersona);
                UsuarioDal.cascada = true;
                if (UsuarioDal.Actualizar(persona.Usuario))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString("Nombre", persona.Nombre);
                    OperationsSql.AddWithValueString("Apellido", persona.Apellido);
                    OperationsSql.AddWithValueString("Sexo", persona.Sexo);
                    OperationsSql.AddWithValueString("Eliminado", persona.Eliminado);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    estado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { UsuarioDal.cascada = false; if (!cascada) { OperationsSql.ExecuteTransactionCommit(); OperationsSql.CloseConnection(); } }
            return estado;
        }
        public static bool Delete(Persona persona)
        {
            return Delete(persona.IdPersona);
        }
        public static bool Delete(Guid idPersona)
        {
            bool estado = false;
            string queryString = @"DELETE  
                                  FROM Persona 
                                  WHERE IdPersona = @IdPersona";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdPersona", idPersona);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return estado;
        }
        public static bool ActivarDesactivar(Guid idPersona, bool desactivar)
        {
            bool estado = false;
            string queryString = @"UPDATE Persona 
                                   SET Eliminado = @Eliminado 
                                   WHERE IdPersona = @IdPersona";
            try
            {
                Persona dataPersona = Obtener_By_Id(idPersona);
                OperationsSql.OpenConnection();
                UsuarioDal.cascada = true;
                OperationsSql.AddWithValueString("Eliminado", desactivar);
                OperationsSql.AddWithValueString("IdPersona", idPersona);
                if (UsuarioDal.ActivarDesactivar(dataPersona.Usuario.IdUsuario, desactivar))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { UsuarioDal.cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        private static Persona ObjectData_To_Persona(Dictionary<string, object> data)
        {
            return new Persona()
            {
                IdPersona = (Guid)data["IdPersona"],
                Nombre = (string)data["Nombre"],
                Apellido = (string)data["Apellido"],
                Sexo = (byte)data["Sexo"],
                Eliminado = (bool)data["Eliminado"],
                Usuario = new Usuario()
                {
                    IdUsuario = (Guid)data["IdUsuario"],
                    Contrasenia = (string)data["Contrasenia"],
                    NombreUsuario = (string)data["NombreUsuario"],
                    Eliminado = (bool)data["Deleted"],
                    Rol = new Rol()
                    {
                        IdRol = (ERol)(byte)data["IdRol"],
                        NombreRol = (string)data["NombreRol"],
                    }
                }
            };
        }
    }
}
