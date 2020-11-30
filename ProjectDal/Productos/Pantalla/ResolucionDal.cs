using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Pantalla
{
    public class ResolucionDal
    {
        private static bool cascada = false;
        public static bool Insert(Resolucion resolucion)
        {
            bool estado = false;
            string query = @"INSERT INTO Resolucion (IdResolucion, NombreResolucion)
                                       Values(@IdResolucion, @NombreResolucion)";
            try
            {
                OperationsSql.OpenConnection();
                cascada = true;
                resolucion.IdResolucion = (byte)(GetLastId() + 1);
                cascada = false;
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdResolucion", resolucion.IdResolucion);
                OperationsSql.AddWithValueString("NombreResolucion", resolucion.NombreResolucion);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception ex)
            {
                Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones.LogError.SetError("Error", ex);
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
            return estado;
        }
        public static Resolucion Get(byte idResolucion)
        {
            Resolucion resolucion = null;
            string query = @"SELECT IdResolucion, NombreResolucion
                             FROM Resolucion
                             WHERE IdResolucion = @IdResolucion";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdResolucion", idResolucion);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    resolucion = Resolucion.Dictionary_To_Resolucion(data);
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones.LogError.SetError("Error", ex);
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
            return resolucion;
        }
        public static List<Resolucion> GetAll()
        {
            List<Resolucion> resolucions = null;
            string query = @"SELECT IdResolucion, NombreResolucion
                             FROM Resolucion
                             ORDER BY NombreResolucion ASC";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    resolucions = new List<Resolucion>();
                    foreach (var item in data)
                    {
                        resolucions.Add(Resolucion.Dictionary_To_Resolucion(item));
                    }
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
            return resolucions;
        }
        public static byte GetLastId()
        {
            byte newId = 0;
            string query = @"SELECT ISNULL(Max(IdResolucion),0) as LastId 
                             FROM Resolucion";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    newId = (byte)data["LastId"];
                }
                if (!cascada)
                {
                    OperationsSql.ExecuteTransactionCommit();
                }
            }
            catch (Exception ex)
            {
                Operaciones.LogError.SetError("Error", ex);
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return newId;
        }
    }
}
