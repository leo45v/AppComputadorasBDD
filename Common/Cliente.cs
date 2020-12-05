using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Cliente : Persona
    {

        public string Email { get; set; }
        public static Cliente ObjectData_To_Client(Dictionary<string, object> data)
        {
            return new Cliente()
            {
                IdPersona = (Guid)data["IdPersona"],
                Nombre = (string)data["Nombre"],
                Apellido = (string)data["Apellido"],
                Sexo = (byte)data["Sexo"],
                Eliminado = (bool)data["Eliminado"],
                Email = (string)data["Email"],
                Usuario = new Usuario()
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
                },
            };
        }
    }
}
