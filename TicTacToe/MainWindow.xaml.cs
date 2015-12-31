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
        private const int Player1 = 1;
        private const int Player2 = 2;

        private Button[,] _btnBoard = null;
        private Board _currentBoard;
        private int _currentPlayer;
        private int _computer;
        private Processor<Node> _processor;

        public MainWindow()
        {
            InitializeComponent();


            _processor = new Processor<Node>();

            _currentPlayer = Player1;
            _computer = Player2;
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
            var originPlayer = _currentPlayer;
            if (_currentBoard[row, col] == 0)
            {
                _currentBoard[row, col] = _currentPlayer;

                if (_currentPlayer == Player1 && _currentPlayer != _computer)
                {
                    btn.Content = "X";
                    _currentPlayer = Player2;
                }
                else if (_currentPlayer == Player2 && _currentPlayer != _computer)
                {
                    btn.Content = "O";
                    _currentPlayer = Player1;
                }
                else return;

                if (_currentPlayer == _computer)
                {
                    ComputerMove(_currentBoard, row, col, originPlayer, _computer);
                    _currentPlayer = originPlayer;
                }
            }
        }

        private void ComputerMove(Board _currentBoard, int row, int col, int originPlayer, int _computer)
        {
            var node = new Node(_currentBoard, row, col, originPlayer, _computer);
            var move = _processor.GetBestMove(node, 3) as Node;

            _currentBoard[move.Row, move.Column] = _computer;
            _btnBoard[move.Row, move.Column].Content = "O";
        }
    }
}
