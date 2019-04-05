using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServeurJEARCE
{
    class Game
    {
        private static byte[] buffer = new byte[1024];
        private Socket player_one;
        private Socket player_two;

        private static bool playing;

        public Game(Socket p1, Socket p2)
        {
            p1 = player_one;
            p2 = player_two;
            playing = true;
            p1.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBackPlayerOne), p1);
            p2.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBackPlayerTwo), p2);
        }

        private static void ReceiveCallBackPlayerOne(IAsyncResult AR)
        {
            while (playing)
            {
                try
                {
                    Socket socket = (Socket)AR.AsyncState;
                    int received = socket.EndReceive(AR);
                    byte[] dataBuf = new byte[received];
                    Array.Copy(buffer, dataBuf, received);
                }
                catch (SocketException)
                {

                }
            }
        }

        private static void ReceiveCallBackPlayerTwo(IAsyncResult AR)
        {
            while (playing)
            {
                try
                {
                    Socket socket = (Socket)AR.AsyncState;
                    int received = socket.EndReceive(AR);
                    byte[] dataBuf = new byte[received];
                    Array.Copy(buffer, dataBuf, received);
                }
                catch (SocketException)
                {

                }
            }
        }
        

        private static void SendToTarget(Socket target, string dataToSend)
        {
            byte[] dataBuf = Encoding.ASCII.GetBytes(dataToSend);
            target.BeginSend(dataBuf, 0, dataBuf.Length, SocketFlags.None, new AsyncCallback(SendCallBack), target);
        }
        private static void SendCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }

    }

}
