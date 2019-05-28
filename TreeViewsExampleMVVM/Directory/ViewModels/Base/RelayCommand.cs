using System;
using System.Windows.Input;

namespace TreeViewsExample
{
    /// <summary>
    /// Comando básico que ejecuta un Action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Miembros privados
        /// <summary>
        /// La acción a ejecutar
        /// </summary>
        private Action _Action;
        #endregion

        #region Eventos públicos

        /// <summary>
        /// El evento que se dispara cuando el valor <see cref="CanExecute(object)"/> ha cambiado
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RelayCommand(Action action)
        {
            this._Action = action;
        }
        #endregion

        #region Métodos del comando


        /// <summary>
        /// Un comando relay puede ejecutarse siempre
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Ejecuta la acción del comando
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _Action();
        }

        #endregion
    }
}
