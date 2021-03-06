﻿using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Personas
{
    public class AdministradorDal
    {
        public static bool Insertar(Administrador administrador)
        {
            bool estado = false;
            string queryString = @"INSERT INTO Administrador 
                                   (IdPersona, FechaContrato)
                                   VALUES
                                   (@IdPersona, @FechaContrato, @FechaNacimiento)";
            try
            {
                OperationsSql.OpenConnection();
                PersonaDal.cascada = true;
                if (PersonaDal.Insertar(administrador as Persona))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString("IdPersona", administrador.IdPersona);
                    OperationsSql.AddWithValueString("FechaContrato", administrador.FechaContrato);
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
        public static Administrador Get_Administrador_By_IdUsuario(Guid idUsuario)
        {
            Administrador administrador = null;
            string queryString = @"SELECT pe.IdPersona, pe.Nombre, pe.Apellido, pe.Sexo, pe.Eliminado, 
                                   cli.FechaContrato, 
                                   usr.IdUsuario, usr.NombreUsuario, usr.Contrasenia, usr.Eliminado as Deleted, usr.IdRol, 
                                   Rol.NombreRol
                                   FROM Persona pe
                                   INNER JOIN Administrador cli ON cli.IdPersona = pe.IdPersona 
                                   INNER JOIN Usuario usr ON usr.IdUsuario = pe.IdUsuario 
                                   INNER JOIN Rol ON Rol.IdRol = usr.IdROl 
                                   WHERE pe.IdUsuario = @idUsuario";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("idUsuario", idUsuario);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    administrador = Administrador.ObjectData_To_Administrador(data);
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                Operaciones.LogError.SetError("Error", ex);
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
            return administrador;
        }
        public static bool ActivarDesactivar(Guid idAdministrador, bool desactivado)
        {
            return PersonaDal.ActivarDesactivar(idAdministrador, desactivado);
        }
        public static bool Update(Administrador administrador)
        {
            bool estado = false;
            string queryString = @"UPDATE Administrador 
                                   SET FechaContrato = @FechaContrato
                                   WHERE IdPersona = @IdPersona";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.AddWithValueString("IdPersona", administrador.IdPersona);
                PersonaDal.cascada = true;
                if (PersonaDal.Update(administrador as Persona))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString("FechaContrato", administrador.FechaContrato);
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
        
    }
}
