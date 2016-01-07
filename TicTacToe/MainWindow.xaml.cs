using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Minimax;
using TicTacToe.AI;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] _btnBoard = null;
        private Board _currentBoard;
        private Processor _processor;

        public MainWindow()
        {
            InitializeComponent();

            _processor = new Processor(new AlphaBetaEvaluator(-1000, 1000));

            _currentBoard = new Board();
            _btnBoard = new Button[3, 3];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    _btnBoard[row, col] = new Button();

                    grid.Children.Add(_btnBoard[row, col]);
                    Grid.SetRow(_btnBoard[row, col], row);
                    Grid.SetColumn(_btnBoard[row, col], col);
                    _btnBoard[row, col].FontSize = 48;

                    _btnBoard[row, col].Click += Button_Click;
                }
            }
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var row = Grid.GetRow(btn);
            var col = Grid.GetColumn(btn);
            if (_currentBoard[row, col] == 0)
            {
                _currentBoard[row, col] = (int)PlayerType.Min;
                btn.Content = "X";
                ComputerMove(_currentBoard, row, col);
            }
        }

        private void ComputerMove(Board _currentBoard, int row, int col)
        {
            var node = new Node(_currentBoard, row, col, PlayerType.Min, PlayerType.Max);
            var move = _processor.GetBestMove(node, 3) as Node;

            _currentBoard[move.Row, move.Column] = (int)PlayerType.Max;
            _btnBoard[move.Row, move.Column].Content = "O";
        }
    }
}
