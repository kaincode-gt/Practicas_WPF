using System.Collections.ObjectModel;
using System.Linq;

namespace TreeViewsExample
{
    /// <summary>
    /// El modelo vista para la vista del directorio
    /// </summary>
    public class DirectoryStructureViewModel
    {
        /// <summary>
        /// Lista de todos los directorios de la máquina
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public DirectoryStructureViewModel()
        {
            //Obtiene las unidades lógicas
            var children = DirectoryStructure.GetLogicalDrives();

            //Crea el modelo vista desde los datos
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));

        }
        #endregion
    }
}
