using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Extras
{
    public class Archivo
    {
        public string PathUri { get; set; }
        public string Nombre { get; set; }
        public byte[] Contenido { get; set; }
        public static bool Guardar_Imagen(string pathDirectory, Archivo archivo)
        {
            bool estado = false;
            try
            {
                archivo.Nombre = archivo.PathUri.Substring(archivo.PathUri.LastIndexOf("\\") + 1);
                if (!File.Exists(Path.Combine(pathDirectory, archivo.Nombre)))
                {
                    File.Copy(archivo.PathUri, Path.Combine(pathDirectory, archivo.Nombre), true);
                }
                estado = true;
            }
            catch (Exception)
            {
            }
            return estado;
        }
        public static bool Borrar_Imagen(string file)
        {
            bool estado = false;
            try
            {
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "assets", file);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(pathFile);
                estado = true;
            }
            catch (Exception)
            {
                // Is Already in Use -> this image also was from another product
            }
            return estado;
        }
    }
}
