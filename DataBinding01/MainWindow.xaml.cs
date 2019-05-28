using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBinding01
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int _num;
        private Thread contador;

        public int Numero
        {
            get
            {
                return _num;
            }
            set
            {
                _num = value;
                OnPropertyChanged("Numero");
            }
        }

        public MainWindow()
        {
            DataContext = this;
            Numero = 99;

            InitializeComponent();
            IniciarContador();
        }

        private void IniciarContador()
        {
            contador = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i < 500; i++)
                {
                    Numero++;
                    Thread.Sleep(1000);
                }
            }));
            contador.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string nombre)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombre));
            }
        }

        private void BotonIniciar_Click(object sender, RoutedEventArgs e)
        {
            IniciarContador();
        }

        private void BotonPausar_Click(object sender, RoutedEventArgs e)
        {
            contador.Abort();
        }
    }
}
