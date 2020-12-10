using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;

namespace ComputersWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int inicio = 0, int cantidad = 10, string productoName = "", byte marca = 0, ETipoProducto tipoProducto = ETipoProducto.None)
        {
            Marca ma = null;
            if (marca != 0)
            {
                ma = new Marca() { IdMarca = marca };
            }
            ListaProductos productos = ProductosBrl.GetWithRangeWithFillter(inicio, cantidad, productoName, ma, tipoProducto);
            if (productos != null && productos.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, productos);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "No se encontro Productos" });
        }


        [HttpPost]
        [Route("procesador")]
        public IActionResult Post([FromBody] Procesador value)
        {
            bool estado = ProductosBrl.Procesador.Insert(value);
            if (estado)
            {
                return StatusCode(StatusCodes.Status201Created, new { message = "Procesador Agregado" });
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }
    }
}
