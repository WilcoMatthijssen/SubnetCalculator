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

    public class Cbx
    {
        UInt16 num=420;
        public string Text { get { return String.Format("/{0} \t {1} IPs", num, (1u << (32 - (int)num) )-2); } }
        public UInt16 Value { get { return num; } set { num = (UInt16)value; } }
 
        public override string ToString()
        {
            return "/" + num.ToString(); 
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
            for (UInt16 i = 8; i <= 30; ++i)
            {
                Cbx item = new Cbx();
                item.Value = i;
                prefixCbx.Items.Add(item);
            }
            prefixCbx.SelectedIndex = 0;


            IP.Text = calc.Ip;
            Refresh();
            // MessageBox.Show((prefixCbx.SelectedItem as ComboboxItem).Value.ToString());

        }

        SubnetCalculator calc = new SubnetCalculator();

        private void IP_LostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            calc.Ip = textbox.Text;
            textbox.Text = calc.Ip;
            Refresh();
            
        }


        private void Refresh()
        {
            NetworkIDUI.Tag = calc.NetworkID;
            BroadcastUI.Tag = calc.BroadCast;

            IpRangeUI.Tag = calc.FirstIp + " - " + calc.LastIp;
            
            
            SubnetUI.Tag = calc.SubnetMask;
            WildcardUI.Tag = calc.WildcardMask;
           
            
        }

        private void prefixCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as ComboBox;
            Console.WriteLine(item.SelectedItem);
        }
    }
}
