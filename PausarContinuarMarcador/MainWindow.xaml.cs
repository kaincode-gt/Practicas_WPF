using System;
using System.Collections.Generic;
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

namespace PausarContinuarMarcador
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void contador()
        {
            //if (this.InvokeRequired())
            { }
            etContador.Text = "hola";
          //  MessageBox.Show("Hola");
        }
        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            
            Thread hilo = new Thread(new ThreadStart(contador));
            hilo.Start();
        }
    }
}
