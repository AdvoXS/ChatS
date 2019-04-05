using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace AlwaysChat_Server
{
    class Program
    {
        
        private const int port = 8888;
        static void Main(string[] args)
        {
            //TcpClient tcpClient = new TcpClient();
            //tcpClient.Connect(adress, port);
            //byte[] data = new byte[1024];
            //StringBuilder str = new StringBuilder(); NetworkStream netStr = tcpClient.GetStream();
            TcpListener listener = null;
            try
            {
                
                IPAddress adress = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(adress, port);
                listener.Start();

                while (true)
                {
                    Console.WriteLine("Ожидание подключений... ");

                    // получаем входящее подключение
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");

                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();

                    // сообщение для отправки клиенту
                    string response = "Привет мир";
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.UTF8.GetBytes(response);

                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Отправлено сообщение: {0}", response);
                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
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
