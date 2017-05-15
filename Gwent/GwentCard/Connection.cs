using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Windows.Markup;
using System.Threading;
using System.Windows;
using System.IO;

namespace nGwentCard
{
    public class Connection
    {
        public Battleground battlegnd { get; set; }
        Package ReseivedPackage { get; set; }   
        bool IsConnectionAlive { get; set; }
        NetworkStream stream { get; set; }
        TcpClient client { get; set; }
        Thread receiveThread { get; set; }

        public void InitConnection()
        {
            string Server = ConfigurationManager.AppSettings["ServerIP"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);
            client = new TcpClient();
            ReseivedPackage = null;           
            try
            {
                ConnectToServer(Server,port);
                this.receiveThread = new Thread(new ThreadStart(GetMessages));
                receiveThread.Start();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                battlegnd.EndBattle();
            }
        }

        private void ConnectToServer(string Server, int port)
        {
            client.Connect(Server, port);
            stream = client.GetStream();
        }

        private void GetMessages()
        {
           try
           {
                IsConnectionAlive = true;        
                while(IsConnectionAlive)
                {
                    Package pkg = new Package();            
                    StringBuilder builder = new StringBuilder();
                    do
                    {
                        byte[] bytes = new byte[64];
                        stream.Read(bytes, 0, bytes.Length);
                        builder.Append(Encoding.Default.GetString(bytes));

                    } while (stream.DataAvailable);
                    string str = builder.ToString().TrimEnd('\0');

                    pkg = XamlReader.Parse(str) as Package;
                    ProcessPackage(pkg, battlegnd);
                }
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {         
                MessageBox.Show(ex.Message);
                battlegnd.Control.Dispatcher.Invoke(() =>
                {
                    battlegnd.EndBattle();
                });
            }
        }

        public void CloseConnection()
        {
            if (stream != null) stream.Close();
            if (client != null) client.Close();
            if (receiveThread != null) receiveThread.Abort();
        }

        private void ProcessPackage(Package pkg,Battleground battlegnd)
        {
           
            if (pkg is ICommandable)
            {
                ICommandable Command = pkg as ICommandable;
                ProcessCommand(pkg, Command);
            }
            if (pkg is ISimple)
            {
                ISimple Simple = pkg as ISimple;
                ProcessSimple(pkg, Simple);
            }
        }

        private void ProcessSimple(Package pkg, ISimple Simple)
        {
            battlegnd.SelectedCardID = Simple.SelectedCardID;
            battlegnd.AffectedCardID = Simple.EffectedCardID;
            battlegnd.PerformSpecialAbility(Simple.SelectedCardID);
            SendGoodCommand();
        }

        private void SendGoodCommand()
        {
            NetCommandPackage GoodPkg = new NetCommandPackage();
            GoodPkg.Command = ConfigurationManager.AppSettings["StartGameCommand"];
            SendMessage(GoodPkg);
        }

        private void ProcessCommand(Package pkg, ICommandable Command)
        {

            if (Command.Command == ConfigurationManager.AppSettings["StartGameCommand"])
            {
                NetCommandPackage SyncPackage = new NetCommandPackage();
                SyncPackage.Command = ConfigurationManager.AppSettings["SyncGameCommand"];
                SyncPackage.InDeckCardCount = battlegnd.InHandCards.Count;
                SyncPackage.InHandCardCount = battlegnd.InStackCards.Count;
                SyncPackage.Scope = battlegnd.UserCardsPower;


                SendMessage(SyncPackage);
            }
            else if (Command.Command == ConfigurationManager.AppSettings["SyncGameCommand"])
            {
                battlegnd.OponentInHandCardCount = pkg.InHandCardCount;
                battlegnd.OponentStackCardCount = pkg.InDeckCardCount;
                battlegnd.OponentCardPower = pkg.Scope;
                SendGoodCommand();
            }
            else if (Command.Command == ConfigurationManager.AppSettings["TurnWaitGameCommand"])
            {
                battlegnd.Control.Dispatcher.Invoke(() =>
                {
                    battlegnd.ShowNotMessage("Ходит ваш опонент, подождите");
                    battlegnd.PlayGroundGrid.IsEnabled = false;
                });
                SendGoodCommand();
            }
            else if (Command.Command == ConfigurationManager.AppSettings["StartTurnGameCommand"])
            {
                battlegnd.Control.Dispatcher.Invoke(() =>
                {
                    battlegnd.ShowNotMessage("Ваш ход");
                    battlegnd.PlayGroundGrid.IsEnabled = true;
                });
                SendGoodCommand();
            }                    
        }

        private void SendMessage(Package Message)
        {

            string Str = XamlWriter.Save(Message);
            byte[] buff = Encoding.Default.GetBytes(Str);
            stream.Write(buff, 0, buff.Length);

        }
    }
}
