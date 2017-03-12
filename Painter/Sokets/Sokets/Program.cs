using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Management;

namespace Sokets
{
    class Program
    {
        static private void Server()
        {
            IPHostEntry HostIP = Dns.GetHostEntry("localhost");
            IPAddress IP = HostIP.AddressList[0];
            IPEndPoint EndPoint = new IPEndPoint(IP, 7777);

            Socket Listener = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Listener.Bind(EndPoint);
                Listener.Listen(10);
                Console.WriteLine("Server Online...");
                while (true)
                {                    
                    Socket Client = Listener.Accept();
                    Console.WriteLine("Оправка информации на: "+Client.RemoteEndPoint);
                    byte[] outarr;


                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher("root\\CIMV2",
                         "Select * From Win32_UserAccount");
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(String.Format("AccountType : {0}\n", Convert.ToString(obj["AccountType"])));
                        builder.Append(String.Format("Caption : {0}\n", obj["Caption"]));
                        builder.Append(String.Format("Description : {0}\n", obj["Description"]));
                        builder.Append(String.Format("Disabled : {0}\n", Convert.ToString(obj["Disabled"])));
                        builder.Append(String.Format("Domain : {0}\n", obj["Domain"]));
                        builder.Append(String.Format("FullName : {0}\n", obj["FullName"]));
                        builder.Append(String.Format("InstallDate : {0}\n", Convert.ToString(obj["InstallDate"])));
                        builder.Append(String.Format("LocalAccount : {0}\n", Convert.ToString(obj["LocalAccount"])));
                        builder.Append(String.Format("Name : {0}\n", Convert.ToString(obj["Name"])));
                        builder.Append(String.Format("PasswordChangeable : {0}\n", Convert.ToString(obj["PasswordChangeable"])));
                        builder.Append(String.Format("PasswordExpires : {0}\n", Convert.ToString(obj["PasswordExpires"])));
                        builder.Append(String.Format("PasswordRequired : {0}\n", Convert.ToString(obj["PasswordRequired"])));
                        builder.Append(String.Format("SID : {0}\n", obj["SID"]));
                        builder.Append(String.Format("SIDType : {0}\n", Convert.ToString(obj["SIDType"])));
                        builder.Append(String.Format("Status : {0}\n", obj["Status"]));
                        builder.Append(String.Format("---------------------------------"));

                        string reply = builder.ToString();

                        outarr = Encoding.Unicode.GetBytes(reply);
                        Client.Send(outarr);

                    }

                    outarr = Encoding.Unicode.GetBytes("<End>");
                    Client.Send(outarr);
                    Client.Shutdown(SocketShutdown.Both);
                    Client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
        static private void Client()
        {
            try
            {
                byte[] bytes = new byte[2048];
                IPHostEntry HostIP = Dns.GetHostEntry("localhost");
                IPAddress IP = HostIP.AddressList[0];
                IPEndPoint EndPoint = new IPEndPoint(IP, 7777);

                Socket sender = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(EndPoint);
                string tmpStr;
                bool Reserved = false;
                while (!Reserved)
                {
                    int bytesRec = sender.Receive(bytes);
                    tmpStr = Encoding.Unicode.GetString(bytes, 0, bytesRec);
                    Console.WriteLine(tmpStr);
                    if (tmpStr.IndexOf("<End>") != -1) Reserved = true;
                }
                
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Chouse...");
                Console.WriteLine("1) Server");
                Console.WriteLine("2) GetInf");

  
                string inp = Console.ReadLine();
                try
                {
                    int i = Convert.ToInt32(inp);
                    switch (i)
                    {
                        case 1:
                            Server();
                            break;
                        case 2:
                            Client();
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Введите число!!(1-2)");
                }
            }
            

        }
    }
}
