using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace TreeViewsExample
{
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Propierties
        /// <summary>
        /// El tipo de este elemento
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// La ruta completa del elemento
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// El nombre de este elemento
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(FullPath); } }

        /// <summary>
        /// Lista de todos los hijos contenidos por el elemento
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indica si el elemento puede expandirse
        /// </summary>
        public bool CanExpand { get; set; }

        /// <summary>
        /// Indica si el elemento actual está expandido
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                //Si la IU manda la orden de expandirse
                if (value == true)
                    //Encuentra todos los hijos
                    Expand();
                else
                    this.ClearChildren();
            }
        }
        #endregion

        public ICommand ExpandCommand { get; set; }

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="fullPath">La ruta completa del elemento</param>
        /// <param name="type">El tipo del elemento</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            //Crea el comando
            this.ExpandCommand = new RelayCommand(Expand);

            //Establece la ruta y el tipo
            this.FullPath = fullPath;
            this.Type = type;

            //Configura los hijos
            this.ClearChildren();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Elimina todos los hijos de la lista, añadiendo un elemento vacío para mostrar el icono expandido si se requiere
        /// </summary>
        private void ClearChildren()
        {
            //Borra elementos hijos
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            //Muestra la flecha de expansion si no es un archivo
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }


        #endregion

        /// <summary>
        /// Expande este directorio y encuentra todos los hijos
        /// </summary>
        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);

            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
                
        }
    }
}
