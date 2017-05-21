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
        public int Scope { get; set; }
        public int Health { get; set; }
        public bool IsConnectionLost { get; set; }
        public bool IsTurnEnd { get; set; }



        public GwentClient(TcpClient NewClient, GwentServer server)
        {
            this.Id = Guid.NewGuid().ToString();
            this.GwentServer = server;
            this.Client = NewClient;
            this.ClientStream = NewClient.GetStream();
            this.Health = 2;
            this.Scope = 0;
        }

        public Package GetMessage()
        {
            Package pkg = new Package();
            StringBuilder builder = new StringBuilder();
            short Size = 0;
            byte[] SizeBytes = new byte[2];
            ClientStream.Read(SizeBytes, 0, 2);
            Size = BitConverter.ToInt16(SizeBytes,0);          
            byte[] buff = new byte[Size];
            ClientStream.Read(buff, 0, buff.Length);
            Console.WriteLine("Пришёл пакет размером :" + buff.Length);
            builder.Append(Encoding.Default.GetString(buff));
            string str = builder.ToString().TrimEnd('\0');        
            pkg = XamlReader.Parse(str) as Package;
            return pkg;                            
        }
        
       public void SendMessage(Package pkg)
        {
            string Str = XamlWriter.Save(pkg);
            byte[] buff = Encoding.Default.GetBytes(Str);           
            short Size = (short)buff.Length;
            byte[] BSize = BitConverter.GetBytes(Size);
            ClientStream.Write(BSize, 0, BSize.Length);
            ClientStream.Write(buff, 0, buff.Length);
            Console.WriteLine("Отправлен пакет размером :" + buff.Length);
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
