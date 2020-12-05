using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas
{
    public class ClienteDal
    {
        public static bool cascada = false;
        public static bool Insertar(Cliente cliente)
        {
            bool estado = false;
            string queryString = @"INSERT INTO Cliente
                                   (IdPersona, Email) 
                                   VALUES
                                   (@IdPersona, @Email)";
            try
            {
                OperationsSql.OpenConnection();
                PersonaDal.cascada = true;
                if (PersonaDal.Insertar(cliente as Persona))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString("Email", cliente.Email);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                Operaciones.LogError.SetError("Error", ex);
            }
            finally
            {
                PersonaDal.cascada = false;
                OperationsSql.CloseConnection();
            }
            return estado;
        }
        public static Cliente Get_Cliente_By_IdCliente(Guid idCliente)
        {
            Cliente cliente = null;
            string queryString = @"SELECT pe.IdPersona, pe.Nombre, pe.Apellido, pe.Sexo, pe.Eliminado, 
                                   cli.Email, 
                                   usr.IdUsuario, usr.NombreUsuario, usr.Contrasenia, usr.Eliminado as Deleted, usr.IdRol, 
                                   Rol.NombreRol
                                   FROM Persona pe
                                   INNER JOIN Cliente cli ON cli.IdPersona = pe.IdPersona 
                                   INNER JOIN Usuario usr ON usr.IdUsuario = pe.IdUsuario 
                                   INNER JOIN Rol ON Rol.IdRol = usr.IdROl
                                   WHERE pe.IdPersona = @IdPersona";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdPersona", idCliente);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    cliente = Cliente.ObjectData_To_Client(data);
                }
                if (!cascada) { OperationsSql.ExecuteTransactionCommit(); }
            }
            catch (Exception ex)
            {
                Operaciones.LogError.SetError("Error", ex);
                if (!cascada) { OperationsSql.ExecuteTransactionCancel(); }
            }
            finally
            {
                if (!cascada) { OperationsSql.CloseConnection(); }
            }
            return cliente;
        }
        public static Cliente Get_Cliente_By_IdUsuario(Guid idUsuario)
        {
            Cliente cliente = null;
            string queryString = @"SELECT pe.IdPersona, pe.Nombre, pe.Apellido, pe.Sexo, pe.Eliminado, 
                                   cli.Email, 
                                   usr.IdUsuario, usr.NombreUsuario, usr.Contrasenia, usr.Eliminado as Deleted, usr.IdRol, 
                                   Rol.NombreRol
                                   FROM Persona pe
                                   INNER JOIN Cliente cli ON cli.IdPersona = pe.IdPersona 
                                   INNER JOIN Usuario usr ON usr.IdUsuario = pe.IdUsuario 
                                   INNER JOIN Rol ON Rol.IdRol = usr.IdROl
                                   WHERE pe.IdUsuario = @IdUsuario";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdUsuario", idUsuario);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    cliente = Cliente.ObjectData_To_Client(data);
                }
                if (!cascada) { OperationsSql.ExecuteTransactionCommit(); }
            }
            catch (Exception ex)
            {
                Operaciones.LogError.SetError("Error", ex);
                if (!cascada) { OperationsSql.ExecuteTransactionCancel(); }
            }
            finally
            {
                if (!cascada) { OperationsSql.CloseConnection(); }
            }
            return cliente;
        }
        public static List<Cliente> GetClientes()
        {
            List<Cliente> clientes = null;
            string queryString = @"SELECT pe.IdPersona, pe.Nombre, pe.Apellido, pe.Sexo, pe.Eliminado, 
                                   cli.Email, 
                                   usr.IdUsuario, usr.NombreUsuario, usr.Contrasenia, usr.Eliminado as Deleted, usr.IdRol, 
                                   Rol.NombreRol
                                   FROM Persona pe
                                   INNER JOIN Cliente cli ON cli.IdPersona = pe.IdPersona
                                   INNER JOIN Usuario usr ON usr.IdUsuario = pe.idUsuario
                                   INNER JOIN Rol ON Rol.IdRol = usr.IdROl";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    clientes = new List<Cliente>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        clientes.Add(Cliente.ObjectData_To_Client(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                Operaciones.LogError.SetError("Error", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return clientes;
        }
        public static bool Update(Cliente cliente)
        {
            bool estado = false;
            string queryString = @"UPDATE Cliente 
                                   SET Email = @Email
                                   WHERE IdPersona = @IdPersona";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.AddWithValueString("IdPersona", cliente.IdPersona);
                PersonaDal.cascada = true;
                if (PersonaDal.Update(cliente as Persona))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString("Email", cliente.Email);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                }
                estado = true;
            }
            catch (Exception ex)
            {
                Operaciones.LogError.SetError("Error", ex);
            }
            finally { PersonaDal.cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        public static bool ActivarDesactivar(Guid idCliente, bool desactivado)
        {
            return PersonaDal.ActivarDesactivar(idCliente, desactivado);
        }
    }
}
