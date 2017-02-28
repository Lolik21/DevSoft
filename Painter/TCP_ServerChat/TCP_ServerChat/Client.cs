using System;
using System.Net.Sockets;
using System.Text;

namespace TCP_ServerChat
{
    class Client
    {        
        public NetworkStream stream { get; private set; }
        public string ID { get; private set; }
        private TcpClient CurrClient;
        private Server Server;
        string UserName;


        public Client(TcpClient CurrClient, Server server)
        {
            this.CurrClient = CurrClient;
            ID = Guid.NewGuid().ToString();
            this.Server = server;
            this.Server.AddClient(this);
        }
        public void ServiseClient()
        {
            try
            {
                stream = CurrClient.GetStream();
                string InMessage = ResiveMessage();
                UserName = InMessage;
                InMessage = UserName + " подключился к серверу.";
                Server.SendToAll(InMessage, this.ID);
                Console.WriteLine(InMessage);
                bool IsConnected = true;

                while(IsConnected)
                {
                    try
                    {
                        InMessage = ResiveMessage();
                        InMessage = String.Format("{0}: {1}", UserName, InMessage);
                        Console.WriteLine(InMessage);
                        Server.SendToAll(InMessage, this.ID);
                    }
                    catch
                    {
                        InMessage = String.Format("{0}: отключился ", UserName);
                        Console.WriteLine(InMessage);
                        Server.SendToAll(InMessage, this.ID);
                        IsConnected = false;
                    }
               }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Server.CloseConnection(this);
                if (stream != null)
                    stream.Close();
                if (CurrClient != null)
                    CurrClient.Close();
            }

           

        }

        public void Close()
        {
            if (stream != null)
                 stream.Close();
            if (CurrClient != null)
                 CurrClient.Close();
        }

        private string ResiveMessage()
        {
            byte[] InArr = new byte[64];
            StringBuilder strBulder = new StringBuilder();
            int MessageLength = 0;
            do
            {
                MessageLength = stream.Read(InArr, 0, InArr.Length);
                strBulder.Append(Encoding.Unicode.GetString(InArr,0,MessageLength));
            } while (stream.DataAvailable);

            return strBulder.ToString();

        }
    }
}
