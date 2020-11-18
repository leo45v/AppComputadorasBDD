
using System;

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
    }
}
