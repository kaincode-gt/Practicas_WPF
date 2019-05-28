using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewsExample
{
    /// <summary>
    /// Información sobre un elemento de directorio: disco, carpeta o archivo.
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// El tipo de este elemento
        /// </summary>
        public DirectoryItemType Type {get; set;}

        /// <summary>
        /// La ruta absoluta del elemento
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// El nombre de este elemento
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(FullPath); } }

    }
}
