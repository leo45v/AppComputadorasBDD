using System;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public bool Eliminado { get; set; }

        /// <summary>
        /// Property for Rol
        /// </summary>
        public Rol Rol { get; set; }


    }
}
