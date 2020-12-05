using System;
using System.Collections.Generic;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas
{
    public class ListaProductos : List<Producto>
    {
        private Dictionary<Guid, int> listaProductosRepetidos = new Dictionary<Guid, int>();

        public new void Add(Producto producto)
        {
            if (listaProductosRepetidos.ContainsKey(producto.IdProducto))
            {
                listaProductosRepetidos[producto.IdProducto] += 1;
            }
            else
            {
                listaProductosRepetidos.Add(producto.IdProducto, 1);
            }
            base.Add(producto);
        }
        public new void RemoveAt(int index)
        {
            if (this.Count > index)
            {
                Guid idProducto = this[index].IdProducto;
                listaProductosRepetidos.Remove(idProducto);
                base.RemoveAt(index);
            }
        }
        public new void Remove(Producto producto)
        {
            if (producto != null)
            {
                int index = this.IndexOf(producto);
                if (this.Count > index)
                {
                    Guid idProducto = this[index].IdProducto;
                    listaProductosRepetidos.Remove(idProducto);
                }
                base.Remove(producto);
            }
        }
        public decimal CostoTotal
        {
            get
            {
                decimal costo = new Decimal(0.0);
                foreach (Producto item in this)
                {
                    costo += item.PrecioUnidad;
                }
                return costo;
            }
        }


        public string ObtenerNombre(Guid idProducto)
        {
            foreach (var item in this)
            {
                if (item.IdProducto == idProducto)
                {
                    return item.Nombre;
                }
            }
            return null;
        }

        public Dictionary<Guid, int> SimplificarById
        {
            get
            {
                return listaProductosRepetidos;
            }
        }

        public void InsertarComputadora(Computadora computadora)
        {
            if (!(computadora.Fuente is null))
            {
                this.Add(computadora.Fuente);
            }
            if (!(computadora.Procesador is null))
            {
                this.Add(computadora.Procesador);
            }
            if (!(computadora.TarjetaGrafica is null))
            {
                this.Add(computadora.TarjetaGrafica);
            }
            if (!(computadora.Gabinete is null))
            {
                this.Add(computadora.Gabinete);
            }
            if (!(computadora.Monitor is null))
            {
                this.Add(computadora.Monitor);
            }
            if (!(computadora.PlacaBase is null))
            {
                this.Add(computadora.PlacaBase);
            }
            if (!(computadora.Almacenamientos is null))
            {
                foreach (var item in computadora.Almacenamientos)
                {
                    this.Add(item);
                }
            }
            if (!(computadora.Rams is null))
            {
                foreach (var item in computadora.Rams)
                {
                    this.Add(item);
                }
            }
        }
    }
}