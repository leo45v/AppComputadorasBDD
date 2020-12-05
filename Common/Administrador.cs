using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
   public class Administrador : Persona
    {
        public DateTime FechaContrato { get; set; }
        public static Administrador ObjectData_To_Administrador(Dictionary<string, object> data)
        {
            return new Administrador()
            {
                IdPersona = (Guid)data["IdPersona"],
                Nombre = (string)data["Nombre"],
                Apellido = (string)data["Apellido"],
                Sexo = (byte)data["Sexo"],
                Eliminado = (bool)data["Eliminado"],
                FechaContrato = (DateTime)data["FechaContrato"],
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
