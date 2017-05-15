using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nGwentCard;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Gwent_Server
{
    class GwentServer
    {
        TcpListener tcpListener;
        List<GwentClient> ClientsQuery = new List<GwentClient>();
        public bool IsStartingPackage(Package Package)
        {
            if (Package is ICommandable)
            {
                ICommandable Command = Package as ICommandable;
                if (Command.Command == ConfigurationManager.AppSettings["StartGameCommand"])
                {
                    return true;
                }             
            }
            return false;
        }

        private void AddToQuery(GwentClient Client)
        {
            ClientsQuery.Add(Client);
        }

        public void WaitForConnection()
        {
            try
            {
                InitServer();
                Console.WriteLine("Сервер запущен..");
                while (true)
                {
                    TcpClient NewClient = tcpListener.AcceptTcpClient();
                    Console.WriteLine("Поключился клиент : " + NewClient.Client.RemoteEndPoint.ToString());
                    GwentClient ClientObj = new GwentClient(NewClient, this);
                    if (ClientsQuery.Count >= 1)
                    {
                        List<GwentClient> Pair = FindPairs(ClientObj, ClientsQuery);
                        Console.WriteLine("Клиенты обьеденились в группу : " + Pair[0].Client.Client.RemoteEndPoint.ToString()+" "+ Pair[1].Client.Client.RemoteEndPoint.ToString());
                        Thread NewThread = new Thread(() => Game(Pair));
                        NewThread.Start();
                    }
                    else
                    {
                        if (ClientObj != null) 
                        {
                            Console.WriteLine("В очередь добавлен клиент : " + NewClient.Client.RemoteEndPoint.ToString());
                            ClientsQuery.Add(ClientObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void GetSyncResponse(List<GwentClient> Clients)
        {
            try
            {
                Package pkg1 = Clients[0].GetMessage();
                Package pkg2 = Clients[1].GetMessage();

                SendToClient(Clients[0], pkg2);
                SendToClient(Clients[1], pkg1);
            }
            catch
            {
                throw;
            }
             
        }

        private bool SendToClient(GwentClient Client,Package pkg)
        {
            Client.SendMessage(pkg);
            Package RecievedRkg = Client.GetMessage();
            if ((RecievedRkg as ICommandable).Command != ConfigurationManager.AppSettings["IsGood"])
                return false;
            else return true;
        }

        private Package GetFromClient(GwentClient Client)
        {
            Package RecivedPkg = Client.GetMessage();
            NetCommandPackage OkPakage = new NetCommandPackage();
            OkPakage.Command = ConfigurationManager.AppSettings["IsGood"];
            Client.SendMessage(OkPakage);
            return RecivedPkg;
        }

        private void Game(List<GwentClient> Clients)
        {
            Random rnd = new Random();
            int WhoIsTerm = rnd.Next(1);
            bool IsConnectionLost = false;
            try
            {
                NetCommandPackage StartGamePackage = new NetCommandPackage();
                StartGamePackage.Command = ConfigurationManager.AppSettings["StartGameCommand"];

                Clients[0].SendMessage(StartGamePackage);
                Clients[1].SendMessage(StartGamePackage);
                Console.WriteLine("Клиентам отправлено уведосление о начале игры");

                GetSyncResponse(Clients);
                Console.WriteLine("Клиенты Синхронизировались");



                NetCommandPackage TurnStart = new NetCommandPackage();
                TurnStart.Command = ConfigurationManager.AppSettings["StartTurnGameCommand"];
                NetCommandPackage TurnWait = new NetCommandPackage();
                TurnWait.Command = ConfigurationManager.AppSettings["TurnWaitGameCommand"];
                Package CurrPkg;
                while (!IsConnectionLost)
                {
                    int NextIsTerm = (WhoIsTerm + 1) % 2;

                    SendToClient(Clients[WhoIsTerm], TurnStart);
                    SendToClient(Clients[NextIsTerm], TurnWait);

                    do
                    {
                        CurrPkg = GetFromClient(Clients[WhoIsTerm]);
                        if (CurrPkg is ICommandable) ProcessCommand(Clients, CurrPkg as ICommandable, WhoIsTerm, ref IsConnectionLost);
                        else ProcessSimple(Clients, CurrPkg, WhoIsTerm);

                    } while (TurnEndMessageIsGiven(CurrPkg) || IsConnectionLost);

                  
                    WhoIsTerm = NextIsTerm;
                }
                Console.WriteLine("Сессия закончилась");
                Clients[0].Dispose();
                Clients[1].Dispose();
            }           
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Clients[0].Dispose();
                Clients[1].Dispose();
            }
            
        }

        private bool TurnEndMessageIsGiven(Package Pakage)
        {
            if (Pakage is ICommandable)
            {
                if ((Pakage as ICommandable).Command == ConfigurationManager.AppSettings["TurnEndGameCommand"])
                    return true;             
            }
            return false;
        }

        private void ProcessSimple(List<GwentClient> Pair, Package Simple, int WhoIsTerm)
        {
            int NextIsTerm = (WhoIsTerm + 1) % 2;
            SendToClient(Pair[NextIsTerm], Simple);
        }

        private void ProcessCommand(List<GwentClient> Pair, ICommandable Command, int WhoIsTerm,ref bool IsConnectionLost)
        {
            int NextIsTerm = (WhoIsTerm + 1) % 2;
                   
            if (Command.Command == ConfigurationManager.AppSettings["LeaveGameCommand"])
            {
                IsConnectionLost = true;
            }
            else
            if (Command.Command == ConfigurationManager.AppSettings["PassedGameCommand"])
            {
                Pair[WhoIsTerm].IsPassed = true;
                NetCommandPackage Passed = new NetCommandPackage();
                Passed.Command = ConfigurationManager.AppSettings["PassedGameCommand"];
                SendToClient(Pair[NextIsTerm], Passed);
            }        
                          
        }

        private void InitInfo(Package pkg, GwentClient Client)
        {
            Client.Scope = pkg.Scope;
            Client.InDeckCardCount = pkg.InDeckCardCount;
            Client.InHandCardCount = pkg.InHandCardCount;
        }

        private List<GwentClient> FindPairs(GwentClient client, List<GwentClient> Query)
        {
            List<GwentClient> Pair = new List<GwentClient>();
            Pair.Add(client);
            Pair.Add(ClientsQuery[0]);
            ClientsQuery.RemoveAt(0);
            return Pair;
        }

        private void InitServer()
        {
            IPAddress adr = IPAddress.Parse(ConfigurationManager.AppSettings["ServerIP"]);
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);
            tcpListener = new TcpListener(adr, port);
            tcpListener.Start();
        }

    }
}
