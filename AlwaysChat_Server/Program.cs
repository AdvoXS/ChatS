using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace AlwaysChat_Server
{
    class Program
    {
        
        private const int port = 8888;
        static void Main(string[] args)
        {
            TcpListener listener = null;
            try
            {
                
                IPAddress adress = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(adress, port);
                listener.Start();

                Console.WriteLine("Ожидание подключений...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);
                    Console.WriteLine("Клиент подключен!");
                    // создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(clientObject.clientAction));
                    clientThread.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }
    }
}
