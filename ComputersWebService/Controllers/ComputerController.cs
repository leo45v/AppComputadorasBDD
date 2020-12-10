using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.ComputadoraBuild;

namespace ComputersWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComputerController : ControllerBase
    {

        private readonly ConfigurationBuildComputer requisitosComputadora = new ConfigurationBuildComputer();


        [HttpGet]
        [Route("build")]
        public IActionResult Get(decimal presupuesto = 500, TipoComputadora tipoComputadora = TipoComputadora.Estudio)
        {
            requisitosComputadora.CambiarTipoComputadora = tipoComputadora;
            ComputadoraBuildBrl.Presupuesto = presupuesto;
            Computadora nuevaComputadora = ComputadoraBuildBrl.ObtenerComputadoraRecomendada(requisitosComputadora.Requisitos.ComputadoraX);
            if (nuevaComputadora != null)
            {
                return StatusCode(StatusCodes.Status200OK, nuevaComputadora);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "No se consiguio armar una computadora" });
        }
    }
}
