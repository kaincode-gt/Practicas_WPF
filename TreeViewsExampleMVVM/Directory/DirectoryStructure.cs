using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TreeViewsExample
{
    /// <summary>
    /// Ayudante para la gestión de la estructura de directorios
    /// </summary>
    public static class DirectoryStructure
    {

        /// <summary>
        /// Encuentra el nombre de un archivo o directorio desde una ruta
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            //Obtiene todos las unidades lógicas de la máquina
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Obtiene los elementos hijos del directorio
        /// </summary>
        /// <param name="fullPath">La ruta completa del directorio</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            //Crea una ruta en blando para los directorios
            var items = new List<DirectoryItem>();

            #region Obtener Directorios

            //Intenta obtener los directorios
            //ignorando todos los problemas que pueden aparecer
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));

            }
            catch { }
            #endregion

            #region Obtener ficheros
            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));

            }
            catch { }
            #endregion

            return items;
        }




        #region Helpers


        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            //Invierte todas las barras de la ruta
            var normalizedPath = path.Replace('/', '\\');

            //Encuentra la última barra de la ruta
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //Si no encuentra ninguna barra, devuelve la ruta completa (es un archivo)
            if (lastIndex < 0)
                return path;

            //Deuvle el nombre después de la última barra
            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}
