using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;


namespace MAC
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkInterface[] NetInt = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface tmpNet in NetInt)
            {
                if (tmpNet.GetPhysicalAddress().ToString() != string.Empty)
                {
                    Console.WriteLine(tmpNet.Name);
                    Console.WriteLine("MAC адрес : " + tmpNet.GetPhysicalAddress().ToString());
                }                          
            }
            Console.ReadLine();
        }
      
          

        
    }
}
