using System;
using System.Collections.Generic;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

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
        public static Usuario Dictionary_A_Usuario(Dictionary<string, object> data)
        {
            return new Usuario()
            {
                IdUsuario = (Guid)data["IdUsuario"],
                Contrasenia = (string)data["Contrasenia"],
                NombreUsuario = (string)data["NombreUsuario"],
                Eliminado = (bool)data["Deleted"],
                Rol = new Rol()
                {
                    IdRol = (ERol)(byte)data["IdRol"],
                    NombreRol = (string)data["NombreRol"],
                }
            };
        }
    }
}
