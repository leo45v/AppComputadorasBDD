using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Pantalla
{
    public class RatioDal
    {
        private static bool cascada = false;
        public static bool Insert(Ratio ratio)
        {
            bool estado = false;
            string query = @"INSERT INTO Ratio (IdRatio, NombreRatio)
                                       Values(@IdRatio, @TamNombreRatio)";
            try
            {
                OperationsSql.OpenConnection();
                cascada = true;
                ratio.IdRatio = (byte)(GetLastId() + 1);
                cascada = false;
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdRatio", ratio.IdRatio);
                OperationsSql.AddWithValueString("TamNombreRatio", ratio.NombreRatio);
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
        public static Ratio Get(byte idRatio)
        {
            Ratio ratio = null;
            string query = @"SELECT IdRatio, NombreRatio
                             FROM Ratio
                             WHERE IdRatio = @IdRatio";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdRatio", idRatio);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    ratio = Ratio.Dictionary_To_Ratio(data);
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
            return ratio;
        }
        public static List<Ratio> GetAll()
        {
            List<Ratio> ratios = null;
            string query = @"SELECT IdRatio, NombreRatio
                             FROM Ratio
                             ORDER BY NombreRatio ASC";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    ratios = new List<Ratio>();
                    foreach (var item in data)
                    {
                        ratios.Add(Ratio.Dictionary_To_Ratio(item));
                    }
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
            return ratios;
        }
        public static byte GetLastId()
        {
            byte newId = 0;
            string query = @"SELECT ISNULL(Max(IdRatio),0) as LastId 
                             FROM Ratio";
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
                Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones.LogError.SetError("Error", ex);
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return newId;
        }
    }
}
