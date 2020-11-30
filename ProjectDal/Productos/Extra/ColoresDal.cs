using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.Extra
{
    public class ColoresDal
    {
        private static bool cascada = false;
        public static bool Insert(Colores colores)
        {
            bool estado = false;
            string query = @"INSERT INTO Color (IdColor, ColorRgb, Nombre)
                                                 Values(@IdColor, @ColorRgb, @Nombre)";
            try
            {
                OperationsSql.OpenConnection();
                cascada = true;
                colores.IdColor = (byte)(GetLastId() + 1);
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdColor", colores.IdColor);
                OperationsSql.AddWithValueString("ColorRgb", colores.Color_ToString);
                OperationsSql.AddWithValueString("Nombre", colores.Nombre);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Insertar un Color", ex);
            }
            finally { cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        public static List<Colores> GetAll()
        {
            List<Colores> colores = null;
            string query = @"SELECT co.IdColor, co.ColorRgb, co.Nombre
                             FROM Color co";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    colores = new List<Colores>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        string[] rgb = item["ColorRgb"].ToString().Split(",");
                        colores.Add(new Colores()
                        {
                            IdColor = (short)item["IdColor"],
                            Nombre = (string)item["Nombre"],
                            ColorRGB = Color.FromArgb(255, int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]))
                        });
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                LogError.SetError("Problemas al Obtener los Colores", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return colores;
        }
        public static byte GetLastId()
        {
            byte newId = 0;
            string query = @"SELECT ISNULL(Max(IdColor),0) as LastId 
                             FROM Color";
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
                LogError.SetError("Problema al Obtener el ultimo Id de Color", ex);
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return newId;
        }
    }
}
