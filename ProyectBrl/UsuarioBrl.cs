using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class UsuarioBrl
    {
        public static void Insertar(Usuario usuario)
        {
            UsuarioDal.Insertar(usuario);
        }
        public static Usuario Seleccionar(Guid idUsuario)
        {
            return UsuarioDal.Obtener(idUsuario);
        }
        public static Usuario Seleccionar(Usuario usuario)
        {
            return UsuarioDal.Obtener(usuario);
        }
        public static List<Usuario> Seleccionar_Todo()
        {
            return UsuarioDal.Obtener_Lista_Usuarios();
        }
        public static bool Borrar(Guid idUsuario)
        {
            return UsuarioDal.Borrar_Por_Id(idUsuario);
        }
        public static bool Borrar(Usuario usuario)
        {
            return UsuarioDal.Borrar(usuario);
        }
        public static bool Actualizar(Usuario usuario)
        {
            return UsuarioDal.Actualizar(usuario);
        }
        public static Guid Obtener_Id_Usuario(Usuario usuario)
        {
            return UsuarioDal.Obtener_Id_By_Password_Username(usuario.NombreUsuario, usuario.Contrasenia);
        }
        public static Guid Obtener_Id_Usuario(string userName, string password)
        {
            return UsuarioDal.Obtener_Id_By_Password_Username(userName, password);
        }
        public static bool NombreUsuario_Libre(string nombreUsuario)
        {
            return UsuarioDal.NombreUsuario_Libre(nombreUsuario);
        }
        public static Rol GetRol(Guid idPersona)
        {
            return UsuarioDal.GetRol(idPersona);
        }
    }
}
