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
using MensErgerJe.View_Model;

namespace MensErgerJe.View
{
    /// <summary>
    /// Interaction logic for RoundButton.xaml
    /// </summary>
    public partial class RoundButton : UserControl
    {
        private Field field;
        private View_Model.BoardView_Model vm;

        public RoundButton(Field newField, View_Model.BoardView_Model newvm)
        {
            vm = newvm;
            field = newField;
            InitializeComponent();
        }

        private void Clicked(object sender, RoutedEventArgs e)
        {
            vm.Field = field;
        }
    }
}
