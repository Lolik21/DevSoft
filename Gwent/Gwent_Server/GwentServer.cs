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
                        Console.WriteLine("Клиенты обьеденились в группу : " + 
                            Pair[0].Client.Client.RemoteEndPoint.ToString()+" "+ Pair[1].Client.Client.RemoteEndPoint.ToString());
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
            return RecivedPkg;
        }

        private void Game(List<GwentClient> Clients)
        {
            Random rnd = new Random();
            int WhoIsTerm = rnd.Next(1);
            int NextIsTerm = (WhoIsTerm + 1) % 2;
            try
            {
                InitBattle(Clients, WhoIsTerm, NextIsTerm);
                NetCommandPackage TurnStart = new NetCommandPackage();
                TurnStart.Command = ConfigurationManager.AppSettings["StartTurnGameCommand"];
                NetCommandPackage TurnWait = new NetCommandPackage();
                TurnWait.Command = ConfigurationManager.AppSettings["TurnWaitGameCommand"];
                NextIsTerm = (WhoIsTerm + 1) % 2;
                while (!Clients[WhoIsTerm].IsConnectionLost && !Clients[NextIsTerm].IsConnectionLost)
                {
                    if (!Clients[WhoIsTerm].IsPassed)
                    {
                        SendToClient(Clients[WhoIsTerm], TurnStart);
                        SendToClient(Clients[NextIsTerm], TurnWait);
                        ClientTurn(Clients, WhoIsTerm, NextIsTerm);
                    }                                 
                    WhoIsTerm = NextIsTerm;
                    NextIsTerm = (WhoIsTerm + 1) % 2;
                    if (Clients[WhoIsTerm].IsPassed && Clients[NextIsTerm].IsPassed)
                    {
                        int WhoWin = GetWhoWin(Clients,WhoIsTerm,NextIsTerm);
                        int WhoLost = (WhoWin + 1) % 2;                       
                        Clients[WhoLost].Health--;
                        ProccesRoundRezults(Clients, WhoWin, WhoLost);
                        Clients[WhoLost].IsPassed = false;
                        Clients[WhoWin].IsPassed = false;
                    }                    
                }
                
                Console.WriteLine("Сессия закончилась");
                Clients[0].Dispose();
                Clients[1].Dispose();
            }           
            catch (Exception Exeption)
            {
                Console.WriteLine(Exeption.Message);
                ProcessExeption(Clients, WhoIsTerm, NextIsTerm);
            }
            
        }

        private void ClientTurn(List<GwentClient> Clients, int WhoIsTerm, int NextIsTerm)
        {
            Package CurrPkg;
            do
            {
                CurrPkg = GetFromClient(Clients[WhoIsTerm]);
                if (CurrPkg is ICommandable)
                    ProcessCommand(Clients, CurrPkg as NetCommandPackage, WhoIsTerm);
                else ProcessSimple(Clients, CurrPkg, WhoIsTerm);

            } while (!Clients[WhoIsTerm].IsTurnEnd);
        }

        private void InitBattle(List<GwentClient> Clients, int WhoIsTerm, int NextIsTerm)
        {
            NetCommandPackage StartGamePackage = new NetCommandPackage();
            StartGamePackage.Command = ConfigurationManager.AppSettings["StartGameCommand"];
            Clients[0].SendMessage(StartGamePackage);
            Clients[1].SendMessage(StartGamePackage);
            Console.WriteLine("Клиентам отправлено уведосление о начале игры");
            GetSyncResponse(Clients);
            Console.WriteLine("Клиенты Синхронизировались");
           
        }


        private void ProccesRoundRezults(List<GwentClient> Clients, int WhoWin, int WhoLost)
        {
            if (Clients[WhoLost].Health == 0)
            {
                NetCommandPackage LostGameCommand = new NetCommandPackage();
                LostGameCommand.Command = ConfigurationManager.AppSettings["LostGameCommand"];
                SendToClient(Clients[WhoLost], LostGameCommand);
                NetCommandPackage WinGameCommand = new NetCommandPackage();
                WinGameCommand.Command = ConfigurationManager.AppSettings["WinGameCommand"];
                SendToClient(Clients[WhoWin], WinGameCommand);
                Clients[WhoWin].IsConnectionLost = true;
                Clients[WhoLost].IsConnectionLost = true;

            }
            else
            {
                NetCommandPackage LostRoundCommand = new NetCommandPackage();
                LostRoundCommand.Command = ConfigurationManager.AppSettings["LostRoundCommand"];
                SendToClient(Clients[WhoLost], LostRoundCommand);
                NetCommandPackage WinRoundCommand = new NetCommandPackage();
                WinRoundCommand.Command = ConfigurationManager.AppSettings["WinRoundCommand"];
                SendToClient(Clients[WhoWin], WinRoundCommand);
            }
        }

        private int GetWhoWin (List<GwentClient> Clients, int WhoIsTerm, int NextIsTerm)
        {
            int WhoWin;
            Random rnd = new Random();
            if (Clients[WhoIsTerm].Scope == Clients[NextIsTerm].Scope)
            {
                WhoWin = rnd.Next(2);
            }
            if (Clients[WhoIsTerm].Scope > Clients[NextIsTerm].Scope)
            {
                WhoWin = WhoIsTerm;
            }
            else
            {
                WhoWin = NextIsTerm;
            }
            return WhoWin;
        }

        private void ProcessExeption(List<GwentClient> Clients, int WhoIsTerm, int NextIsTerm)
        {
            try
            {
                NetCommandPackage Error = new NetCommandPackage();
                Error.Command = ConfigurationManager.AppSettings["ConnectionLost"];
                Clients[NextIsTerm].SendMessage(Error);
                Clients[WhoIsTerm].SendMessage(Error);
            }
            catch (Exception InnerExeption)
            {
                Console.WriteLine(InnerExeption.Message);
            }
            finally
            {
                Clients[0].Dispose();
                Clients[1].Dispose();
            }
        }



        private void ProcessCommand(List<GwentClient> Pair, NetCommandPackage Command, int WhoIsTerm)
        {
            int NextIsTerm = (WhoIsTerm + 1) % 2;

            if (Command.Command == ConfigurationManager.AppSettings["LeaveGameCommand"])
            {
                NetCommandPackage LeavePkg = new NetCommandPackage();
                LeavePkg.Command = ConfigurationManager.AppSettings["LeaveGameCommand"];
                Pair[NextIsTerm].SendMessage(LeavePkg);
                Pair[WhoIsTerm].IsConnectionLost = true;
                Pair[WhoIsTerm].IsTurnEnd = true;
            }
            else
            if (Command.Command == ConfigurationManager.AppSettings["PassedGameCommand"])
            {
                Pair[WhoIsTerm].IsPassed = true;
                NetCommandPackage Passed = new NetCommandPackage();
                Passed.Command = ConfigurationManager.AppSettings["PassedGameCommand"];
                SendToClient(Pair[NextIsTerm], Passed);
            }
            else
            if (Command.Command == ConfigurationManager.AppSettings["TurnEndGameCommand"])
            {
                Pair[WhoIsTerm].IsTurnEnd = true;
            }
            if (Command.Command == ConfigurationManager.AppSettings["SyncGameCommand"])
            {                
                SendToClient(Pair[NextIsTerm], Command);
                Pair[WhoIsTerm].Scope = Command.Scope;
            }


        }

        private void ProcessSimple(List<GwentClient> Pair, Package Simple, int WhoIsTerm)
        {
            int NextIsTerm = (WhoIsTerm + 1) % 2;
            SendToClient(Pair[NextIsTerm], Simple);
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
