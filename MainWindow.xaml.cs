using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using SubnetCalculator;

namespace SubnetCalculatorGUI
{

    public class ComboboxItem
    {
        UInt16 num;
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }





    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        SubnetCalculator calc = new SubnetCalculator();

        private void IP_LostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            calc.Ip = textbox.Text;
            textbox.Text = calc.Ip;
        }
    }
}
