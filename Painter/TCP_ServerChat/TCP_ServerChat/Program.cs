
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCP_ServerChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server;
            Thread ServThread;
            try
            {
                server = new Server();
                ServThread = new Thread(new ThreadStart(server.Listen));
                ServThread.Start();
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }


        }
    }
}
