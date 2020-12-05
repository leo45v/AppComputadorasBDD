using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Reservas
{
    public class ReservasDal
    {
        public static bool cascada = false;
        public static List<string> Errores { get; private set; } = new List<string>();
        public static bool Insert(Reserva reserva)
        {
            bool estado = false;
            string queryString = @"INSERT INTO Reserva(IdReserva, IdPersona, FechaReserva, Eliminado, Recogido) 
                                                 VALUES(@IdReserva, @IdPersona, @FechaReserva, @Eliminado, @Recogido)";
            try
            {
                Errores.Clear();
                OperationsSql.OpenConnection();
                cascada = true;
                reserva.IdReserva = GetLastId() + 1;
                reserva.FechaReserva = DateTime.Now;
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdReserva", reserva.IdReserva);
                OperationsSql.AddWithValueString("IdPersona", reserva.Cliente.IdPersona);
                OperationsSql.AddWithValueString("FechaReserva", reserva.FechaReserva);
                OperationsSql.AddWithValueString("Eliminado", reserva.Eliminado);
                OperationsSql.AddWithValueString("Recogido", reserva.Recogido);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                if (DetalleReserva(reserva.Productos, reserva.IdReserva))
                {
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
                else
                {
                    OperationsSql.ExecuteTransactionCancel();
                }
            }
            catch (Exception)
            {
                Errores.Add("No se consiguio agregar la reserva");
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Agregar Una reserva de productos");
            }
            finally { cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }

        public static bool Delete(long idReserva)
        {
            return ActivarDesactivar(idReserva, true);
        }
        public static bool Habilitar(long idReserva)
        {
            return ActivarDesactivar(idReserva, false);
        }
        public static bool ActivarDesactivar(long idReserva, bool eliminado)
        {
            bool estado = false;
            string queryString = @"UPDATE Reserva 
                                   SET Eliminado = @Eliminado 
                                   WHERE IdReserva = @IdReserva";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdReserva", idReserva);
                OperationsSql.AddWithValueString("Eliminado", eliminado);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Activar/Desactivar la Reserva", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return estado;
        }
        public static bool Recoger(long idReserva)
        {
            return Recogido_SiNo(idReserva, true);
        }
        public static bool NoRecogido(long idReserva)
        {
            return Recogido_SiNo(idReserva, false);
        }
        public static bool Recogido_SiNo(long idReserva, bool recogido)
        {
            bool estado = false;
            string queryString = @"UPDATE Reserva 
                                   SET Recogido = @Recogido 
                                   WHERE IdReserva = @IdReserva";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdReserva", idReserva);
                OperationsSql.AddWithValueString("Recogido", recogido);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Actualizar la Reserva -> Recogido", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return estado;
        }
        public static bool DetalleReserva(ListaProductos productos, long idReserva)
        {
            bool estado = false;
            try
            {
                Dictionary<Guid, int> listaProductos = productos.SimplificarById;
                bool error = false;
                foreach (var (idProducto, stock) in listaProductos)
                {
                    ProductosDal.cascada = true;
                    int stockActual = ProductosDal.GetStock(idProducto);
                    int math = stockActual - stock;
                    if (math < 0)
                    {
                        LogError.SetError(String.Format("El Producto {0} No tiene stock suficiente {1}:{2}", idProducto, stockActual, stock));
                        Errores.Add(String.Format("El Producto \"{0}\" No tiene stock suficiente {1}:{2}", productos.ObtenerNombre(idProducto), stockActual, stock));
                        listaProductos = new Dictionary<Guid, int>();
                        error = true;
                        break;
                    }
                    if (InsertarProducto_Reserva(idReserva, idProducto, stock))
                    {
                        if (!ProductosDal.ModificarStock(idProducto, math))
                        {
                            error = true;
                            Errores.Add(String.Format("El producto \"{0}\" no tiene suficiente stock", productos.ObtenerNombre(idProducto)));
                            break;
                        }
                    }
                    else
                    {
                        Errores.Add(String.Format("No se Consiguio Insertar el producto \"{0}\" en la reserva", productos.ObtenerNombre(idProducto)));
                        error = true;
                        break;
                    }
                }
                if (!error) { estado = true; }
            }
            catch (Exception)
            {
                Errores.Add("Problemas al agregar la reserva");
                LogError.SetError("Problemas al Agregar Una reserva de productos");
            }
            finally { ProductosDal.cascada = false; }
            return estado;
        }
        public static bool InsertarProducto_Reserva(long idReserva, Guid idProducto, int cantidad)
        {
            bool estado = false;
            string queryString = @"INSERT INTO DetalleReserva(IdProducto, IdReserva, Cantidad) 
                                                 VALUES(@IdProducto, @IdReserva, @Cantidad)";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                OperationsSql.AddWithValueString("IdReserva", idReserva);
                OperationsSql.AddWithValueString("Cantidad", cantidad);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                if (!cascada) { OperationsSql.ExecuteTransactionCommit(); }
                estado = true;
            }
            catch (Exception ex)
            {
                if (!cascada) { OperationsSql.ExecuteTransactionCancel(); }
                LogError.SetError("Problemas al Agregar un detalle reserva", ex);
            }
            finally { OperationsSql.RemoveValueParams(); if (!cascada) { OperationsSql.CloseConnection(); } }
            return estado;
        }
        public static long GetLastId()
        {
            long newId = 0;
            string query = @"SELECT ISNULL(MAX(IdReserva),0) as LastId 
                             FROM Reserva";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    newId = (long)data["LastId"];
                }
                if (!cascada)
                {
                    OperationsSql.ExecuteTransactionCommit();
                }
            }
            catch (Exception ex)
            {
                LogError.SetError("Problema al Obtener el ultimo Id de Reservas", ex);
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return newId;
        }
        public static List<Reserva> GetReservas(Guid idCliente)
        {
            List<Reserva> reservas = null;
            string query = @"SELECT r.IdReserva
                             FROM Reserva r
                             WHERE r.IdPersona = @IdPersona AND r.Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdPersona", idCliente);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                OperationsSql.ExecuteTransactionCommit();
                if (data != null)
                {
                    reservas = new List<Reserva>();
                    foreach (var item in data)
                    {
                        Reserva reserva = Get((long)item["IdReserva"]);
                        if (reserva != null)
                        {
                            reservas.Add(reserva);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.SetError("Problema al Obtener las Reservas de un Cliente", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return reservas;
        }
        public static Reserva Get(long idReserva)
        {
            Reserva reserva = null;
            string query = @"SELECT r.IdReserva, r.IdPersona, r.FechaReserva, r.Eliminado, r.Recogido  
                             FROM Reserva r
                             WHERE r.Eliminado = 0 AND r.IdReserva = @IdReserva";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdReserva", idReserva);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    Guid idCliente = (Guid)data["IdPersona"];
                    ClienteDal.cascada = true;
                    Cliente cli = ClienteDal.Get_Cliente_By_IdCliente(idCliente);
                    ClienteDal.cascada = false;
                    if (cli != null)
                    {
                        cascada = true;
                        ListaProductos productos = GetProductos(idReserva);
                        cascada = false;
                        if (productos != null)
                        {
                            reserva = new Reserva()
                            {
                                Cliente = cli,
                                Eliminado = (bool)data["Eliminado"],
                                FechaReserva = (DateTime)data["FechaReserva"],
                                Recogido = (bool)data["Recogido"],
                                IdReserva = (long)data["IdReserva"],
                                Productos = productos
                            };
                        }
                    }
                    // GET SOPORTE PROCESADORES
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Obtener el Producto -> Placa Base");
            }
            finally { OperationsSql.CloseConnection(); }
            return reserva;
        }
        public static ListaProductos GetProductos(long idReserva)
        {
            ListaProductos productos = null;
            string queryString = @"SELECT dr.IdReserva, dr.IdProducto, dr.Cantidad 
                                   FROM DetalleReserva dr
                                   INNER JOIN Reserva r ON r.IdReserva = dr.IdReserva
                                   WHERE dr.IdReserva = @IdReserva AND r.Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdReserva", idReserva);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    productos = new ListaProductos();
                    foreach (Dictionary<string, object> item in data)
                    {
                        ProductosDal.cascada = true;
                        Producto nuevoProducto = ProductosDal.Get((Guid)item["IdProducto"]);
                        ProductosDal.cascada = true;
                        for (int i = 0; i < (long)item["Cantidad"]; i++)
                        {
                            if (nuevoProducto != null)
                            {
                                productos.Add(nuevoProducto);
                            }
                        }
                    }
                }
                if (!cascada) { OperationsSql.ExecuteTransactionCommit(); }
            }
            catch (Exception ex)
            {
                LogError.SetError("Error", ex);
                if (!cascada) { OperationsSql.CloseConnection(); }
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return productos;
        }
        public static bool QuitarProducto_Reserva(Guid idProducto, long idReserva)
        {
            bool estado = false;
            try
            {
                cascada = true;
                DetalleReserva detalleReserva = GetDetalleReserva(idProducto, idReserva);
                if (detalleReserva != null)
                {
                    if (detalleReserva.Cantidad > 1)
                    {
                        estado = QuitarProducto_Decrease(idProducto, idReserva, detalleReserva.Cantidad - 1);
                    }
                    else
                    {
                        estado = QuitarProducto_Borrado(idProducto, idReserva);
                    }
                }
                cascada = false;
            }
            catch (Exception ex)
            {
                LogError.SetError("Problemas al Eliminar un Producto de la Reserva", ex);
            }
            return estado;
        }

        public static bool QuitarProducto_Borrado(Guid idProducto, long idReserva)
        {
            bool estado = false;
            string queryString = @"DELETE DetalleReserva 
                                   WHERE IdReserva = @IdReserva AND IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdReserva", idReserva);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Eliminar un Producto de la Reserva", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return estado;
        }
        public static bool QuitarProducto_Decrease(Guid idProducto, long idReserva, long cantidad)
        {
            bool estado = false;
            string queryString = @"UPDATE DetalleReserva 
                                   SET Cantidad = @Cantidad 
                                   WHERE IdReserva = @IdReserva AND IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdReserva", idReserva);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                OperationsSql.AddWithValueString("Cantidad", cantidad);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Eliminar un Producto de la Reserva", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return estado;
        }

        public static DetalleReserva GetDetalleReserva(Guid idProducto, long idReserva)
        {
            DetalleReserva detalleReserva = null;
            string queryString = @"SELECT dr.Cantidad 
                                   FROM DetalleReserva dr
                                   INNER JOIN Reserva r ON r.IdReserva = dr.IdReserva
                                   WHERE dr.IdReserva = @IdReserva AND dr.IdProducto = @IdProducto AND r.Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdReserva", idReserva);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    detalleReserva = new DetalleReserva()
                    {
                        Producto = new Producto() { IdProducto = idProducto },
                        Cantidad = (long)data["Cantidad"],
                        Reserva = new Reserva()
                        {
                            IdReserva = idReserva
                        }
                    };
                }
                if (!cascada) { OperationsSql.ExecuteTransactionCommit(); }
            }
            catch (Exception ex)
            {
                LogError.SetError("Error", ex);
                if (!cascada) { OperationsSql.CloseConnection(); }
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return detalleReserva;
        }
    }
}
