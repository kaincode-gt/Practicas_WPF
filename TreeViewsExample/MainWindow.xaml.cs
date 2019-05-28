using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TreeViewsExample
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region On Loaded
        /// <summary>
        /// Cuando la aplicación se inicia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Obtiene todos las unidades lógicas de la máquina
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Crea un nuevo elemento del árbol.
                var item = new TreeViewItem()
                {
                    //Configura la etiqueta
                    Header = drive,
                    // y la ruta
                    Tag = drive
                };

                //Añade un elemento hijo vacío.
                item.Items.Add(null);

                //Expande el elemento
                item.Expanded += Folder_Expanded;

                //Añade el elemento de la unidad a la raíz.
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folded Expanded
        /// <summary>
        /// Encuentra nombre del archivo o directorio en la ruta completa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Chequeo inicial

            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //Limpia el elemento vacío.
            item.Items.Clear();

            //Obtiene la ruta completa del directorio
            var fullPath = (string)item.Tag;
            #endregion

            #region Get Folders

            //Crea una ruta en blando para los directorios
            var directories = new List<string>();

            //Intenta obtener los directorios
            //ignorando todos los problemas que pueden aparecer
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);

            }
            catch { }

            directories.ForEach(directoryPath =>
            {
                //Crea directorio del elemento
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath
                };

                subItem.Items.Add(null);
                subItem.Expanded += Folder_Expanded;

                item.Items.Add(subItem);
            });
            #endregion

            #region Objener ficheros

            //Crea una ruta en blando para los ficheros
            var files = new List<string>();

            //Intenta obtener los directorios
            //ignorando todos los problemas que pueden aparecer
            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    files.AddRange(fs);

            }
            catch { }

            files.ForEach(filePath =>
            {
                //Crea directorio del elemento
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath
                };

                item.Items.Add(subItem);
            });
            #endregion

        }
        #endregion

        #region Helpers
        /// <summary>
        /// Encuentra el nombre de un archivo o directorio desde una ruta
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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
