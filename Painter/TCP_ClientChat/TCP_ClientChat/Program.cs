using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCP_ClientChat
{
    class Program
    {
        private const int port = 9999;
        private const string server = "127.0.0.1";
        static TcpClient Client = null;
        static NetworkStream stream = null;
        static void Main(string[] args)
        {
            
            try
            {
                Console.Write("Введите свое имя:");
                string UserName = Console.ReadLine();
                Client = new TcpClient();
                Client.Connect(server, port);
                Console.WriteLine("Успешно подключено...");                
                stream = Client.GetStream();
                
                Console.Write(UserName + ": ");
                String message = Console.ReadLine();
                message = UserName;

                byte[] OutArr = Encoding.Unicode.GetBytes(message);
                stream.Write(OutArr, 0, OutArr.Length);

                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();

                SendMessage();



            }          
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
                Client.Close();
                stream.Close();
            }

            Console.WriteLine("Сеанс завершён...");
            Console.Read();
        }

        static void SendMessage()
        {
            Console.WriteLine("Введите сообщение: ");

            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }

        static void ReceiveMessage()
        {
            try
            {
                while (true)
                {
                    StringBuilder response = new StringBuilder();
                    byte[] InArr = new byte[64];
                    do
                    {
                        int bytes = stream.Read(InArr, 0, InArr.Length);
                        response.Append(Encoding.Unicode.GetString(InArr, 0, bytes));

                    } while (stream.DataAvailable);
                    string message = response.ToString();
                    Console.WriteLine(message);
                }
            }
            catch
            {
                Console.WriteLine("Подключение прервано!");
                Console.ReadLine();
                Close();
            }
        }
        private void Close()
        {
            if (stream != null)
                stream.Close();
            if (Client != null)
                Client.Close();
            Environment.Exit(0);



        }
}
