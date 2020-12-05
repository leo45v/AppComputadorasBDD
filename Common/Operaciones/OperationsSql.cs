using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Extras;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class OperationsSql
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["BDDDIRECT"].ConnectionString;
        private static readonly SqlConnection connection = new SqlConnection() { ConnectionString = connectionString };
        private static readonly SqlCommand command = new SqlCommand() { Connection = connection, CommandType = CommandType.Text };
        private static SqlTransaction transaccion;

        public string MyProperty
        {
            get
            {
                return connectionString;
            }
            set
            {
                connection.ConnectionString = connectionString;
                connectionString = value;
            }
        }

        public static void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public static void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        private static void CreateTransaction()
        {
            if (transaccion == null)
            {
                transaccion = connection.BeginTransaction();
            }
        }
        public static void CreateBasicCommandWithTransaction(string query)
        {
            CreateTransaction();
            command.Transaction = transaccion;
            command.CommandText = query;
        }
        public static void AddWithValueString(string parameter, object value)
        {
            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@" + parameter,
                Value = value
            };
            if (VerificarParameter(param))
            {
                command.Parameters.AddWithValue("@" + parameter, value);
            }

        }
        public static bool VerificarParameter(SqlParameter sqlParameter)
        {
            foreach (SqlParameter item in command.Parameters)
            {
                if (item.ParameterName == sqlParameter.ParameterName)
                {
                    return false;
                }
            }
            return true;
        }
        private static string GetSqlQuery()
        {
            string result = command.CommandText.ToString();
            foreach (SqlParameter p in command.Parameters)
            {
                string isQuted = (p.Value is string) ? "'" : (p.Value is Guid) ? "'" : "";
                string value = (p.Value is bool) ? (Convert.ToInt32((bool)p.Value)).ToString() : p.Value.ToString();
                result = result.Replace(p.ParameterName.ToString(), isQuted + value + isQuted);
            }
            return result;
        }
        public static void ExecuteBasicCommandWithTransaction()
        {
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Operations.WriteLogsRelease("Methods", "SqlException in ExecuteBasicCommand", string.Format("{0} {1}", ex.Message, ex.StackTrace));
                throw new Exception("Se ha producido un error en el método ExecuteBasicCommand(SqlCommand cmd)", ex);
            }
            catch (Exception ex)
            {
                Operations.WriteLogsRelease("Methods", "Exception in ExecuteBasicCommand", string.Format("{0} {1}", ex.Message, ex.StackTrace));
                throw new Exception("Se ha producido un error en el método ExecuteBasicCommand(SqlCommand cmd)", ex);
            }
        }
        public static void ExecuteTransactionCommit()
        {
            if (transaccion != null)
            {
                transaccion.Commit();
                transaccion = null;
                command.Parameters.Clear();
            }
        }
        public static void ExecuteTransactionCancel()
        {
            if (transaccion != null)
            {
                transaccion = null;
                command.Parameters.Clear();
            }
        }
        public static void RemoveValueParams()
        {
            command.Parameters.Clear();
        }
        private static void FillObjectWithProperty(ref object objectTo, string propertyName, object propertyValue)
        {
            Type tOb2 = objectTo.GetType();
            if (tOb2.GetProperty(propertyName) != null)
            {
                tOb2.GetProperty(propertyName).SetValue(objectTo, propertyValue);
            }
        }
        public static Dictionary<string, object> ExecuteReader()
        {
            Dictionary<string, object> data = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    data = GetData_FromSQL(reader);
                }
            }
            return data; //data.Count > 0 ? data : null;
        }
        public static List<Dictionary<string, object>> ExecuteReaderMany()
        {
            List<Dictionary<string, object>> ListData = null;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                ListData = new List<Dictionary<string, object>>();
                while (reader.Read())
                {
                    ListData.Add(GetData_FromSQL(reader));
                }
            }
            return ListData.Count > 0 ? ListData : null;
        }
        private static Dictionary<string, object> GetData_FromSQL(SqlDataReader reader)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (DataRow drow in reader.GetSchemaTable().Rows)
            {
                data.Add(drow.ItemArray[0].ToString(), reader[drow.ItemArray[0].ToString()]);
            }
            return data;
        }
    }
}
