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
using System.Windows.Shapes;

namespace MensErgerJe.View
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        View_Model.BoardView_Model vm;

        public Game()
        {
            InitializeComponent();
            vm = new View_Model.BoardView_Model(PlayField,4);
            DataContext = vm.Board;
            DiceField.DataContext = vm.Board.Dice;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.throwDice();
        }
    }
}
