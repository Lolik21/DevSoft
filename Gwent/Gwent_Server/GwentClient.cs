using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Markup;
using nGwentCard;

namespace Gwent_Server
{
    class GwentClient : IDisposable
    {
        protected string Id { get; private set; }
        protected NetworkStream ClientStream { get; private set; }
        public TcpClient Client;
        private GwentServer GwentServer;
        public bool IsPassed { get; set; }
        public bool IsClientTurn { get; set; }
        public int InHandCardCount { get; set; }
        public int InDeckCardCount { get; set; }
        public int Scope { get; set; }
        public int Health { get; set; }


        public GwentClient(TcpClient NewClient, GwentServer server)
        {
            this.Id = Guid.NewGuid().ToString();
            this.GwentServer = server;
            this.Client = NewClient;
            this.ClientStream = NewClient.GetStream();
            this.IsClientTurn = false;
            this.Health = 2;
        }

        public Package GetMessage()
        {
            Package pkg = new Package();
            StringBuilder builder = new StringBuilder();
            do
            {
                byte[] bytes = new byte[64];
                ClientStream.Read(bytes, 0, bytes.Length);
                builder.Append(Encoding.Default.GetString(bytes));

            } while (ClientStream.DataAvailable);
            string str = builder.ToString().TrimEnd('\0');        
            pkg = XamlReader.Parse(str) as Package;
            return pkg;                            
        }
        
       public void SendMessage(Package pkg)
        {
            string Str = XamlWriter.Save(pkg) + "\0";
            byte[] buff = Encoding.Default.GetBytes(Str);
            ClientStream.Write(buff, 0, buff.Length);
        }

        public void Dispose()
        {
            if (ClientStream != null)
            {
                ClientStream.Close();
            }
            if (Client != null)
            {
                Client.Close();
                GwentServer = null;
            }
        }

        
    }
}
