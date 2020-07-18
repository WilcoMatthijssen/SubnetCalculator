using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubnetCalculatorGUI
{
    class SubnetCalculator
    {
        private UInt32 ip;
        private UInt16 prefix;
        private UInt32 subnetMask;
        private UInt32 firstIp;
        private UInt32 lastIp;

        public SubnetCalculator() { Ip = "192.168.0.1";  Prefix = 24; }

        private UInt32 IpAsUInt32(System.Net.IPAddress ipaddr)
        {
            byte[] bytes = ipaddr.GetAddressBytes();

            // flip big-endian(network order) to little-endian
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }



        public string Ip
        {
            get { return IpToString(ip); }
            set {
                // Check if string be converted to ip
                System.Net.IPAddress newIp;
                if (System.Net.IPAddress.TryParse(value, out newIp)){
                    ip = IpAsUInt32(newIp);
                

                    //Get ip range
                    firstIp = ip & subnetMask;
                    lastIp = firstIp | ( UInt32.MaxValue << prefix );


                    Console.WriteLine("IP ADDR {0}", IpToString(firstIp));
                    Console.WriteLine("SUBNET MASK {0}", IpToString(subnetMask));
                };
                

            }
        }

        
        
        public UInt16 Prefix
        {
            get { return prefix; }
            set
            {
                if (value > 0 && value < 32)
                {
                    prefix = value;
                    subnetMask = UInt32.MaxValue << (32 - value);
                    Ip = ip.ToString();
                }
            }
        }
        private static string IpToString(UInt32 ip)
        {
            byte[] bytes = BitConverter.GetBytes(ip);

            // flip little-endian to big-endian(network order)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return new System.Net.IPAddress(bytes).ToString();
        }




        public string FirstIp
        {
            get { return IpToString(firstIp); }
        }
        public string LastIp
        {
            get { return IpToString(lastIp); }
        }

    }
}
