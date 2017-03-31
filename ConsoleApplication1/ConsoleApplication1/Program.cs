using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;


namespace ConsoleApplication1
{
    class Program
    {
        static string FROM;
        static string DOMAIN;
        static string RET;
        static string DIR_ADDR;
        static void Main(string[] args)
        {
            AcceptClients();

        }

        public static void AcceptClients()
        {
            TcpListener smtpServer = null;
            try
            {
                IPAddress addr = IPAddress.Parse("127.0.0.1");
                smtpServer = new TcpListener(addr, 25);
                smtpServer.Start();
                Console.WriteLine("<<Stmp Server Enabled>>");
                while (true)
                {                    
                    TcpClient NewClient = smtpServer.AcceptTcpClient();
                    Console.WriteLine("Подключён клиент: " + NewClient.Client.RemoteEndPoint);
                    Thread thr = new Thread(new ParameterizedThreadStart(ProssesClient));
                    thr.Start(NewClient);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ProssesClient(object Client)
        {
            TcpClient curr_client = (TcpClient)Client;
            NetworkStream stream = curr_client.GetStream();
            SendResponseType(220, stream);
            bool ISQuit = false;
            byte[] buff = new byte[64];
            string str = "";
            string tmp = "";
            while (!ISQuit)
            {
                try
                {
                    stream.Read(buff, 0, buff.Length);
                    tmp = Encoding.ASCII.GetString(buff);
                    TrimZeros(0, tmp.IndexOf("\n"), tmp, ref str);
                    if (str.IndexOf("QUIT") == 0)
                    {
                        ISQuit = true;
                        Console.WriteLine("Получен QUIT");
                        SendResponseType(221, stream);
                        stream.Close();
                        curr_client.Close();
                    }
                    else
                    ProcessInput(str, stream, curr_client);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ISQuit = true;
                }              
                
            }
           
            if (stream != null) stream.Close();
            if (curr_client != null) curr_client.Close();
            

        }

        public static void TrimZeros(int from,int to,string src,ref string desten)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = from; i<=to; i++)
            {
                builder.Append(src[i]);
            }           
            desten = builder.ToString();            
        }

        public static void SendResponseType(int ResponseN, NetworkStream stream)
        {
            string ServResponse;
           
            switch (ResponseN)
            {
                case 220:
                    ServResponse = "220 localhost Welcome to Email Smtp Server by Ilya v.1.0\r\n";
                    break;
                case 221:
                    ServResponse = "221 127.0.0.1:25 Service closing transmission channel\r\n";
                    break;
                case 250:
                    ServResponse = "250 OK\r\n";
                    break;
                case 354:
                    ServResponse = "354 Start mail input; end with <CRLF>.<CRLF>\r\n";
                    break;
                case 501:
                    ServResponse = "501 Syntax error in parameters or arguments\r\n";
                    break;
                case 502:
                    ServResponse = "502 Command not implemented\r\n";
                    break;
                case 503:
                    ServResponse = "503 Bad sequence of commands\r\n";
                    break;
                case 550:
                    ServResponse = "550 No such user\r\n";
                    break;
                case 551:
                    ServResponse = "551 User not local. Can not forward the mail\r\n";
                    break;
                default:
                    ServResponse = Convert.ToString(ResponseN)+ "what? \r\n";
                    break;                    
            }
            Console.WriteLine("S: "+ServResponse.Trim('\n','\r'));
            SendResponse(ServResponse,stream);

        }

        public static void SendResponse(string ServReponse, NetworkStream stream)
        {
            byte[] buff = Encoding.ASCII.GetBytes(ServReponse);
            stream.Write(buff, 0, buff.Length);
        }
        
        public static void ProcessInput(string Input, NetworkStream stream,TcpClient clinet)
        {
            if (Input.IndexOf("HELO") == 0) ProcessHELO(Input, stream);
            if (Input.IndexOf("EHLO") == 0) ProcessEHLO(Input, stream);
            if (Input.IndexOf("MAIL") == 0) ProcessMAIL(Input, stream);
            if (Input.IndexOf("RCPT") == 0) ProcessRCPT(Input, stream);
            if (Input.IndexOf("DATA") == 0) ProcessDATA(Input, stream, clinet);
        }

        private static void ProcessHELO(string Input, NetworkStream stream)
        {
            Console.WriteLine("S: Принят HELO");
        }
        private static void ProcessEHLO(string Input , NetworkStream stream)
        {
            Console.WriteLine("S: Принят EHLO");
            string domain = Input.Replace("EHLO ","");
            domain = domain.Remove(domain.Length - 2,2);

            if (Directory.Exists(domain))
            {
                Console.WriteLine("S: Обнаружена директория для домена: " + domain);
            }
            else
            {              
                Directory.CreateDirectory(domain);
                Console.WriteLine("S: Директории для домена не было обнаружено");
                Console.WriteLine("S: Создана директория: " + domain);
            }
            DOMAIN = domain;
            SendResponseType(250, stream);
        }
        private static void ProcessMAIL(string Input, NetworkStream stream)
        {
            Console.WriteLine("S: Принят MAIL FROM");
            TrimZeros(Input.IndexOf("<") + 1, Input.IndexOf(">") - 1, Input,ref FROM);
            SendResponseType(250, stream);
        }
        private static void ProcessRCPT(string Input, NetworkStream stream)
        {
            Console.WriteLine("S: Принят RCPT TO");
            TrimZeros(Input.IndexOf("<") + 1, Input.IndexOf(">") - 1, Input, ref RET);
            DIR_ADDR = DOMAIN + '\\' + RET;
            if (Directory.Exists(DIR_ADDR))
            {
                Console.WriteLine("S: Обнаружена директория для адресата: " + RET);
            }
            else
            {
                Directory.CreateDirectory(DIR_ADDR);
                Console.WriteLine("S: Директории для адресата не было обнаружено");
                Console.WriteLine("S: Создана директория: " + RET);
            }
            SendResponseType(250, stream);
        }
        private static void ProcessDATA(string Input, NetworkStream stream, TcpClient client)
        {
            Console.WriteLine("S: Принят DATA");
            SendResponseType(354, stream);
            bool MsgCreated = false;
            int i = 1;
            while (!MsgCreated && i<10)
            {
                
                try
                {
                    string FileName = DIR_ADDR + "\\" + "msg" + Convert.ToString(i) + ".eml";
                    if (File.Exists(FileName))
                    {
                        i++;
                    }
                    else
                    {
                        using (Stream FileS = new FileStream(FileName, FileMode.CreateNew))
                        {
                            using (StreamWriter sr = new StreamWriter(FileS))
                            {
                                sr.WriteLine("Received:");
                                sr.WriteLine("from " + client.Client.RemoteEndPoint);
                                sr.WriteLine("by 127.0.0.1:25");
                                sr.WriteLine("via SMTP");
                                sr.WriteLine("id " + Convert.ToString(i));
                                sr.WriteLine("Return-Path 127.0.0.1:25");
                                sr.WriteLine("Reply-To: " + client.Client.RemoteEndPoint);

                                string tmp = null;
                                byte[] buff = new byte[1000];
                                stream.Read(buff, 0, buff.Length);
                                Input = Encoding.ASCII.GetString(buff);                           
                                TrimZeros(4, Input.IndexOf("\r\n.\r\n") - 5, Input, ref tmp);                                
                                sr.WriteLine(tmp);
                                MsgCreated = true;
                                Console.WriteLine("S: Сообщение получено");
                                SendResponseType(250, stream);                             
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MsgCreated = true;
                }                                                
            }
        }

    }
}
