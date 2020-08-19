using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        bool isInitialized = false;
        SubnetCalculator calc = new SubnetCalculator();

        public MainWindow()
        {
            InitializeComponent();
            isInitialized = true;
 
            // Change all input to default values from calc.
            IP.Text = calc.Ip;
            Prefix.Text = calc.Prefix.ToString();
            PrefixSlider.Value = (double) calc.Prefix;

            // Refresh display with updated values.
            Refresh();
        }

  
        // Refresh display with updated values.
        private void Refresh()
        {
            if (isInitialized)
            { 
                txtbxNetworkID.Text = calc.NetworkID;
                txtbxBroadcast.Text = calc.BroadCast;

                txtbxFirstIP.Text   = calc.FirstIp;
                txtbxLastIP.Text    = calc.LastIp;

                txtbxSubnet.Text    = calc.SubnetMask;
                txtbxWildcard.Text  = calc.WildcardMask;
                txtbxHosts.Text     = calc.Hosts;
            }
            
        }


        // Give changed subnet mask to calc and refresh displayed information.
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Prevent prefix from being changed while selected
            if (!Prefix.IsSelectionActive)
            {
                var slider = sender as Slider;
                calc.Prefix = (ushort) slider.Value;
                Prefix.Text = calc.Prefix.ToString();
                this.Refresh();
            }
        }


        private void InputChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            if(textbox.Name == "IP")
            {
                calc.Ip = IP.Text;
                if (calc.Ip == IP.Text)
                {
                    IP.BorderBrush = System.Windows.Media.Brushes.Green;
                }
                else
                {
                    IP.BorderBrush = System.Windows.Media.Brushes.Red;
                }
            }
            else
            {
                try 
                {
                    calc.Prefix = Convert.ToUInt16(Prefix.Text); 
                }
                catch
                {

                }
                if (calc.Prefix.ToString() == Prefix.Text)
                {
                    PrefixSlider.Value = calc.Prefix;
                    Prefix.BorderBrush = System.Windows.Media.Brushes.Green;
                }
                else
                {
                    Prefix.BorderBrush = System.Windows.Media.Brushes.Red;
                }
            }
            Refresh();

        }

        // Only allow number or dots in text.
        private void InputPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        // Open browser with given hyperlink.
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        
    }
}
