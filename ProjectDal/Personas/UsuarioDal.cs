using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas
{
    public class UsuarioDal
    {
        public static bool cascada = false;
        public static bool Insertar(Usuario usuario)
        {
            bool estado = false;
            string queryString = @"INSERT INTO Usuario(IdUsuario, NombreUsuario, Contrasenia, Eliminado, IdRol) 
                                                VALUES(@IdUsuario, @NombreUsuario, @Contrasenia, @Deleted, @IdRol)";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdUsuario", usuario.IdUsuario);
                OperationsSql.AddWithValueString("NombreUsuario", usuario.NombreUsuario);
                OperationsSql.AddWithValueString("Contrasenia", usuario.Contrasenia);
                OperationsSql.AddWithValueString("Deleted", usuario.Eliminado);
                OperationsSql.AddWithValueString("IdRol", usuario.Rol.IdRol);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                if (!cascada) { OperationsSql.ExecuteTransactionCommit(); }
                estado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return estado;
        }

        public static Usuario Obtener(Usuario usuario)
        {
            return Obtener(usuario.IdUsuario);
        }
        public static Usuario Obtener(Guid IdUsuario)
        {
            Usuario usuario = null;
            string queryString = @"SELECT usr.IdUsuario, usr.NombreUsuario, usr.Contrasenia, usr.Eliminado as Deleted, 
                                   usr.IdRol, Rol.NombreRol 
                                   FROM Usuario usr
                                   INNER JOIN Rol ON Rol.IdRol = usr.IdRol
                                   WHERE IdUsuario = @IdUsuario AND Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdUsuario", IdUsuario);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    usuario = Dictionary_A_Usuario(data);
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
            return usuario;
        }
        public static List<Usuario> Obtener_Lista_Usuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string queryString = @"SELECT usr.IdUsuario, usr.NombreUsuario, usr.Contrasenia, usr.Eliminado as Deleted, 
                                   usr.IdRol, Rol.NombreRol 
                                   FROM Usuario usr
                                   INNER JOIN Rol ON Rol.IdRol = usr.IdRol
                                   WHERE Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                foreach (Dictionary<string, object> item in data)
                {
                    usuarios.Add(Dictionary_A_Usuario(item));
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
            return usuarios;
        }

        public static Guid Obtener_Id_By_Password_Username(string userName, string password)
        {
            Guid idUsuario = Guid.Empty;
            string queryString = @"SELECT IdUsuario
                                   FROM Usuario 
                                   WHERE NombreUsuario = @NombreUsuario AND Contrasenia = @Contrasenia AND Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("NombreUsuario", userName);
                OperationsSql.AddWithValueString("Contrasenia", password);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    idUsuario = (Guid)data["IdUsuario"];
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return idUsuario;
        }
        public static bool Actualizar(Usuario usuario)
        {
            bool estado = false;
            string queryString = @"UPDATE Usuario 
                                   SET NombreUsuario = @NombreUsuario, 
                                       Contrasenia = @Contrasenia, 
                                       Eliminado = @Deleted, 
                                       IdRol = @IdRol 
                                   WHERE IdUsuario = @IdUsuario";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(
                    @"SELECT COUNT(*) as Cantidad 
                      FROM Usuario 
                      WHERE NombreUsuario = @NombreUsuario AND IdUsuario != @IdUsuario");
                OperationsSql.AddWithValueString("IdUsuario", usuario.IdUsuario);
                OperationsSql.AddWithValueString("NombreUsuario", usuario.NombreUsuario);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null && (int)data["Cantidad"] == 0)
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString("Contrasenia", usuario.Contrasenia);
                    OperationsSql.AddWithValueString("Deleted", usuario.Eliminado);
                    OperationsSql.AddWithValueString("IdRol", usuario.Rol.IdRol);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    if (!cascada) { OperationsSql.ExecuteTransactionCommit(); }
                    estado = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return estado;
        }

        public static bool ActivarDesactivar(Usuario usuario, bool desactivar)
        {
            return ActivarDesactivar(usuario.IdUsuario, desactivar);
        }
        public static bool ActivarDesactivar(Guid idUsuario, bool desactivar)
        {
            bool estado = false;
            string queryString = @"UPDATE Usuario 
                                   SET Eliminado = @Eliminado 
                                   WHERE IdUsuario = @IdUsuario";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("Eliminado", desactivar);
                OperationsSql.AddWithValueString("IdUsuario", idUsuario);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                if (!cascada)
                {
                    OperationsSql.ExecuteTransactionCommit();
                }
                estado = true;
            }
            catch (Exception)
            {
                OperationsSql.ExecuteTransactionCancel();
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return estado;
        }
        public static bool NombreUsuario_Libre(string nombreUsuario)
        {
            bool estado = false;
            string queryString = @"SELECT COUNT(*) as Cantidad
                                   FROM Usuario
                                   WHERE NombreUsuario = @NombreUsuario";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("NombreUsuario", nombreUsuario);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    int cant = (int)data["Cantidad"];
                    if (cant == 0)
                    {
                        estado = true;
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { OperationsSql.CloseConnection(); }
            return estado;
        }
        public static Rol GetRol(Guid IdUsuario)
        {
            Rol rol = null;
            string query = @"SELECT Rol.IdRol, Rol.NombreRol 
                                   FROM Usuario usr 
                                   INNER JOIN Rol ON Rol.IdRol = usr.IdRol 
                                   WHERE usr.IdUsuario = @IdUsuario";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdUsuario", IdUsuario);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    rol = new Rol()
                    {
                        IdRol = (ERol)(byte)data["IdRol"],
                        NombreRol = (string)data["NombreRol"]
                    };
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { OperationsSql.CloseConnection(); }
            return rol;
        }
        /*
         * Dictionary<string,object> => Coleccion de Datos
         * Su key o identificador es una cadena (Un texto)
         * Su valor es un objeto osea cualquier cosa, (numero, texto, bool, etc..)
         * ejemplo
         * Dictionary<string,object> nuevoDiccionario = new Dictionary<string,object>();
         * nuevoDiccionario.add("Key","Valor");
         * nuevoDiccionario.add("Key 2",123);
         * 
         * print(nuevo["Key"]) => devuelve -> "Valor"
         * print(nuevo["Key 2"]) => devuelve -> 123
         */
        private static Usuario Dictionary_A_Usuario(Dictionary<string, object> data)
        {
            return new Usuario()
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
            };
        }
    }
}
