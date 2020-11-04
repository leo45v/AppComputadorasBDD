using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class RamDal : ProductosDal
    {
        public static bool Insertar(Ram ram)
        {
            bool estado = false;
            string query = @"INSERT INTO Ram (IdProducto, Memoria, Frecuencia, Latencia)
                                       Values(@IdProducto, @Memoria, @Frecuencia, @Latencia)";
            try
            {
                OperationsSql.OpenConnection();
                Insertar(ram as Producto);
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("Memoria", ram.Memoria);
                OperationsSql.AddWithValueString("Frecuencia", ram.Frecuencia);
                OperationsSql.AddWithValueString("Latencia", ram.Latencia);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
            return estado;
        }

        public static bool Delete_Ram(Guid idProducto)
        {
            bool estado = false;
            string query = @"DELETE FROM Producto WHERE IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
            return estado;
        }
    }

}