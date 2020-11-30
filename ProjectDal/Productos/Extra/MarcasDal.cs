using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Extra
{
    public class MarcasDal
    {
        private static bool cascada = false;
        public static bool Insert(Marca marca)
        {
            bool estado = false;
            string query = @"INSERT INTO Marca (IdMarca, NombreMarca)
                                         Values(@IdMarca, @NombreMarca)";
            try
            {
                OperationsSql.OpenConnection();
                cascada = true;
                marca.IdMarca = (byte)(GetLastId() + 1);
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdMarca", marca.IdMarca);
                OperationsSql.AddWithValueString("NombreMarca", marca.NombreMarca);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                Operaciones.LogError.SetError("Error", ex);
            }
            finally { cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        public static List<Marca> GetAll()
        {
            List<Marca> marcas = null;
            string query = @"SELECT IdMarca, NombreMarca
                             FROM Marca
                             ORDER BY NombreMarca ASC";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    marcas = new List<Marca>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        marcas.Add(new Marca()
                        {
                            IdMarca = (byte)item["IdMarca"],
                            NombreMarca = (string)item["NombreMarca"]
                        });
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                Operaciones.LogError.SetError("Error", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return marcas;
        }
        public static byte GetLastId()
        {
            byte newId = 0;
            string query = @"SELECT ISNULL(Max(IdMarca),0) as LastId 
                             FROM Marca";
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
