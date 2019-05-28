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

namespace BarraDeProgresoPausa
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static AutoResetEvent _event = new AutoResetEvent(false);
        private Thread HiloBarraProgeso;

        private bool paused = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BotonInicio_Click(object sender, RoutedEventArgs e)
        {
            HiloBarraProgeso = new Thread(new ThreadStart(AnimarBarraProgreso));
            HiloBarraProgeso.Start();
            //new Thread(()=> {
                
            //}).Start();
        }

        private void AnimarBarraProgreso()
        {
            BotonInicio.Dispatcher.BeginInvoke(new Action(() => {
                BotonInicio.IsEnabled = false;
                BotonPausar.IsEnabled = true;
            }));

            for (int i = 0; i <= 100; i++)
            {
                if (paused)
                {
                    _event.WaitOne();
                }
                Barra.Dispatcher.BeginInvoke(new Action(() => {
                    Barra.Value = i;
                }));
                Thread.Sleep(50);
            }
            _event.Set();
            BotonInicio.Dispatcher.BeginInvoke(new Action(() => {
                BotonInicio.IsEnabled = true;
                BotonPausar.IsEnabled = false;
            }));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void BotonPausar_Click(object sender, RoutedEventArgs e)
        {
            if (!paused)
            {
                paused = true;
                BotonPausar.Content = "Reanudar";
            }
            else
            {
                paused = false;
                BotonPausar.Content = "Pausar";
                _event.Set();
            }

            //MessageBox.Show("pausado");
        }
    }
}
