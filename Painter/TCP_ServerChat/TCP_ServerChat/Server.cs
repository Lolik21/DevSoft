using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace TCP_ServerChat
{
    class Server
    {
        TcpListener server;
        List<Client> AllClients = new List<Client>();
        public void AddClient(Client ChatClient)
        {
            AllClients.Add(ChatClient);
        }
        public void CloseConnection(Client client)
        {
            client.Close();           
        }
        public void Listen()
        {
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, 9999);
                server.Start();
                Console.WriteLine("Server activated....");
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Client CurrentClient = new Client(client,this);

                    Thread ClientServis = new Thread(new ThreadStart(CurrentClient.ServiseClient));
                    ClientServis.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                DisconectAll();
            }
            finally
            {
                if (server != null)
                    server.Stop();               
            }
        }
        private void DisconectAll()
        {
            server.Stop();
            for (int i = 0; i < AllClients.Count; i++)
            {
                AllClients[i].Close();
            }
            Environment.Exit(0);
        }

        public void SendToAll(string Message, string ID)
        {
            byte[] data = Encoding.Unicode.GetBytes(Message);
            for (int i = 0; i < AllClients.Count; i++)
            {
                if (AllClients[i].ID != ID)
                {
                    AllClients[i].stream.Write(data, 0, data.Length);
                }
            }
        } 


    }
}
