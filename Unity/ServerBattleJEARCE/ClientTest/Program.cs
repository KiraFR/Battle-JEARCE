using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace MultiClient
{
    class Program
    {
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 8181;
        private static Thread receiveRespThread;
        static void Main()
        {
            Console.Title = "Client";
            ConnectToServer();
            RequestLoop();
            Exit();
        }

        private static void ConnectToServer()
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Connection attempt " + attempts);
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException) 
                {
                    Console.Clear();
                }
            }

            Console.Clear();
            Console.WriteLine("Connected");
        }

        private static void RequestLoop()
        {
            Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

            receiveRespThread = new Thread(new ThreadStart(ReceiveResponse));
            receiveRespThread.Start();
            while (true)
            {
                SendRequest();
            }
        }

        private static void Exit()
        {
            SendString("exit");
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        private static void SendRequest()
        {
            string request = Console.ReadLine();
            SendString(request);

            if (request.ToLower() == "exit")
            {
                Exit();
                receiveRespThread.Abort();
            }
        }

        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static void ReceiveResponse()
        {
            while (true)
            {
                var buffer = new byte[2048];
                int received = ClientSocket.Receive(buffer, SocketFlags.None);
                if (received > 0)
                {
                    var data = new byte[received];
                    Array.Copy(buffer, data, received);
                    string text = Encoding.ASCII.GetString(data);
                    Console.WriteLine(text);
                }
            }
        }
    }
}
