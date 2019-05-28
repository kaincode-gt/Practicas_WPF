using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TresEnRaya
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Miembros Privados

        /// <summary>
        /// Guarda la situación actual de todas las casillas del juego actual
        /// </summary>
        private TipoMarca[][] _Resultados;

        /// <summary>
        /// Verdadero si es el turno del jugador 1 (X)
        /// </summary>
        private bool _TurnoJugador;

        /// <summary>
        /// Verdadero si el juego actual ha terminado
        /// </summary>
        private bool _JuegoTerminado;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NuevoJuego();
        }
        #endregion


        /// <summary>
        /// Comienza un juego nuevo y resetea todos los valores anteriores
        /// </summary>
        private void NuevoJuego()
        {
            //Crea un nuevo array de nueve casillas
            _Resultados = new TipoMarca[3][];


            for (var i = 0; i < _Resultados.Length; i++)
            {
                _Resultados[i] = new TipoMarca[3];
            }

            //Se asegura de que es el turno del jugador 1
            _TurnoJugador = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Cambia el fondo y el color frontal a valores por defecto.
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Se asegura de que el juego no ha terminado
            _JuegoTerminado = false;
        }

        /// <summary>
        /// Maneja la pulsación sobre una casilla
        /// </summary>
        /// <param name="sender">La casilla pulsada</param>
        /// <param name="e">El evento de pulsación</param>
        private void Slot_Click(object sender, RoutedEventArgs e)
        {
            //Inicia una nueva partida cuando el juego termina
            if (_JuegoTerminado)
            {
                NuevoJuego();
                return;
            }

            var slot = (Button)sender;

            var columna = Grid.GetColumn(slot);
            var fila = Grid.GetRow(slot);

            //Si la celda está ocupada no hace nada
            if (_Resultados[fila][columna] != TipoMarca.Ninguna)
                return;

            //Coloca el valor del jugador actual en la celda
            _Resultados[fila][columna] = _TurnoJugador? TipoMarca.Cruz : TipoMarca.Cara;

            slot.Content = _TurnoJugador ? "X" : "O";

            if (!_TurnoJugador)
                slot.Foreground = Brushes.Red;
            //Alterna el turno actual
            _TurnoJugador ^= true;

            //Comprueba si hay un ganador
            CompruebaGanador();
        }

        /// <summary>
        /// Comprueba si alguien ha ganado la partida
        /// </summary>
        private void CompruebaGanador()
        {

            CheckearFilas();

            bool tableroCompleto = ChequearTableroCompleto();

            if (tableroCompleto)
            {
                _JuegoTerminado = true;
                slot00.Background = slot01.Background = slot02.Background =
                    slot10.Background = slot11.Background = slot12.Background =
                    slot20.Background = slot21.Background = slot22.Background = Brushes.Orange;
            }

        }

        /// <summary>
        /// Comprueba si existen casillas vacías o el tablero está completo
        /// </summary>
        /// <returns>Verdadero si todas las casillas han sido rellenadas</returns>
        private bool ChequearTableroCompleto()
        {
            for (int i = 0; i < _Resultados.Length; i++)
            {
                for (int j = 0; j < _Resultados[i].Length; j++)
                {
                    if (_Resultados[i][j] == TipoMarca.Ninguna)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void CheckearFilas()
        {
            //Chequear filas
            for (int i = 0; i < _Resultados.Length; i++)
            {
                if (_Resultados[i][0] != TipoMarca.Ninguna && (_Resultados[i][1] & _Resultados[i][2]) == _Resultados[i][0])
                {
                    _JuegoTerminado = true;

                    switch (i)
                    {
                        case 0:
                            slot00.Background = slot01.Background = slot02.Background = Brushes.Yellow;
                            break;
                        case 1:
                            slot10.Background = slot11.Background = slot12.Background = Brushes.Yellow;
                            break;
                        case 2:
                            slot20.Background = slot21.Background = slot22.Background = Brushes.Yellow;
                            break;
                    }
                }
            }
        }
    }
}
