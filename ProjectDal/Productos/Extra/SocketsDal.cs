using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Extra
{
    public class SocketsDal
    {
        private static bool cascada = false;
        public static bool Insert(SocketProcesador socketProcesador)
        {
            bool estado = false;
            string query = @"INSERT INTO SocketProcesador (IdSocket, NombreSocket, Descripcion)
                                                 Values(@IdSocket, @NombreSocket, @Descripcion)";
            try
            {
                OperationsSql.OpenConnection();
                cascada = true;
                socketProcesador.IdSocket = (byte)(GetLastId() + 1);
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdSocket", socketProcesador.IdSocket);
                OperationsSql.AddWithValueString("NombreSocket", socketProcesador.NombreSocket);
                OperationsSql.AddWithValueString("Descripcion", socketProcesador.Descripcion);
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
        public static List<SocketProcesador> GetAll()
        {
            List<SocketProcesador> socketProcesadors = null;
            string query = @"SELECT IdSocket, NombreSocket, Descripcion
                             FROM SocketProcesador";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    socketProcesadors = new List<SocketProcesador>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        socketProcesadors.Add(new SocketProcesador()
                        {
                            IdSocket = (int)item["IdSocket"],
                            NombreSocket = (string)item["NombreSocket"],//(ESocketProcesador)Enum.Parse(typeof(ESocketProcesador), (string)item["NombreSocket"]),
                            Descripcion = (string)item["Descripcion"]
                        });
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                Operaciones.LogError.SetError("Error", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return socketProcesadors;
        }
        public static byte GetLastId()
        {
            byte newId = 0;
            string query = @"SELECT Max(IdSocket) as LastId 
                             FROM SocketProcesador";
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
                OperationsSql.ExecuteTransactionCancel();
                Operaciones.LogError.SetError("Error", ex);
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return newId;
        }
    }
}
