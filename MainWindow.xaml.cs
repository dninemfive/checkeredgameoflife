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

namespace CheckeredGameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _ = new Game(GameBoard, DebugText);
            for(int i = 0; i < Constants.GridSize; i++) for(int j = 0; j < Constants.GridSize; j++)
                {
                    GameBoard.Add(Game.Board[i, j]);
                }
            Player player = new(Colors.Red);
            player.TakeTurn();
        }
    }
}
