using ServeurJEARCE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServeurJEARCE
{
    class Server
    {
        private static List<Socket> clientSocket = new List<Socket>();
        private static List<Game> games = new List<Game>();
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void SetupServer()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8080));
            serverSocket.Listen(10);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        private static void AcceptCallBack(IAsyncResult AR)
        {
            Socket client = serverSocket.EndAccept(AR);
            
            clientSocket.Add(client);
            if(clientSocket.Count > 1)
            {
                new Game(clientSocket[clientSocket.Count - 2], clientSocket[clientSocket.Count - 1]);
            }

            serverSocket.BeginAccept(AcceptCallBack, null);
        }



        /*private void CreateGames()
        {
            Random rand = new Random();
            while (true)
            {
                if(clientSocket.Count > 1)
                {
                    int player1_index = rand.Next(0,clientSocket.Count);
                    Socket player1 = clientSocket[player1_index];
                    clientSocket.Remove(player1);

                    int player2_index = rand.Next(0, clientSocket.Count);
                    Socket player2 = clientSocket[player2_index];
                    clientSocket.Remove(player2);

                    //games.Add(new Game(player1, player2));
                }
            }
        }*/

    }
}
