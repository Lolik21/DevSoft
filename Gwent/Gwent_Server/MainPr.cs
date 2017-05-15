using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nGwentCard;
using System.Net.Sockets;
using System.Net;
using System.Threading;


namespace Gwent_Server
{
    class MainPr
    {
        
        static void Main(string[] args)
        {
            try
            {
                GwentServer server = new GwentServer();
                Thread ServerThread = new Thread(new ThreadStart(server.WaitForConnection));
                ServerThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        


    }
}
