using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServeurJEARCE
{
    class Game
    {
        private Socket player_one;
        private Socket player_two;
        private const int BUFFER_SIZE = 2048;
        private static byte[] buffer = new byte[BUFFER_SIZE];
        private static bool playing;

        public Game(Socket p1, Socket p2)
        {
            player_one = p1;
            player_two = p2;
            playing = true;
            Console.WriteLine("Two clients Connected");
            player_one.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallBackPlayerOne, player_one);
            player_two.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallBackPlayerTwo, player_two);
        }

        public void ReceiveCallBackPlayerTwo(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = socket.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                socket.Close();
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Player two send text: " + text);
            byte[] data = Encoding.ASCII.GetBytes(text);
            player_one.Send(data);

            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallBackPlayerTwo, socket);
        }

        public void ReceiveCallBackPlayerOne(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = socket.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                socket.Close();
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Player one send text: " + text);
            byte[] data = Encoding.ASCII.GetBytes(text);
            player_two.Send(data);

            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallBackPlayerOne, socket);
        }
    }
}
