
using System;
using System.Collections.Generic;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Persona
    {
        public Guid IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public byte Sexo { get; set; }
        public bool Eliminado { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }
        public static Persona ObjectData_To_Persona(Dictionary<string, object> data)
        {
            return new Persona()
            {
                IdPersona = (Guid)data["IdPersona"],
                Nombre = (string)data["Nombre"],
                Apellido = (string)data["Apellido"],
                Sexo = (byte)data["Sexo"],
                Eliminado = (bool)data["Eliminado"],
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
                }
            };
        }
    }
}
