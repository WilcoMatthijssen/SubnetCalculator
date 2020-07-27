using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubnetCalculatorGUI
{
    class SubnetCalculator
    {
        //Decimal for 192.168.0.1
        private UInt32 ip = 3232235521;
        // Prefix
        private UInt16 prefix = 24;

        private UInt32 subnetMask;
        private UInt32 wildcardMask;

        private UInt32 firstIp;
        private UInt32 lastIp;

        private UInt32 networkID;
        private UInt32 broadcast;

        public SubnetCalculator() { calculate(); }
        public string Ip
        {
            get { return IpToString(ip); }
            set {
                // Check if string be converted to ip
                System.Net.IPAddress newIp;
                if (System.Net.IPAddress.TryParse(value, out newIp)){
                    ip = IpAsUInt32(newIp);

                    calculate();
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
 
                    calculate();
                }
            }
        }


        public void calculate()
        {
            //prefix stuff
            subnetMask = UInt32.MaxValue << (32 - prefix);
            wildcardMask = ~subnetMask;

            //ip range
            networkID = ip & subnetMask;
            broadcast = networkID | wildcardMask;

            //ip range
            firstIp = networkID + 1;
            lastIp = broadcast - 1;

       
        }


        private string IpToString(UInt32 ip)
        {
            byte[] bytes = BitConverter.GetBytes(ip);

            // flip little-endian to big-endian(network order)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return new System.Net.IPAddress(bytes).ToString();
        }

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


        public string FirstIp
        {
            get { return IpToString(firstIp); }
        }
        public string LastIp
        {
            get { return IpToString(lastIp); }
        }
        public string NetworkID
        {
            get { return IpToString(networkID); }
        }
        public string BroadCast
        {
            get { return IpToString(broadcast); }
        }
        public string WildcardMask
        {
            get { return IpToString(wildcardMask); }
        }
        public string SubnetMask
        {
            get { return IpToString(subnetMask); }
        }


    }
}
