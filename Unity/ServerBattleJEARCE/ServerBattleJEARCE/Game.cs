using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using testDLL;


namespace ServeurJEARCE
{
    class Game
    {
        private Socket player_one;
        private Socket player_two;

        private static bool playing;
        private bool placement;
        private bool init = false;
        private BinaryFormatter formatter = new BinaryFormatter();
        public Game(Socket p1, Socket p2)
        {
            player_one = p1;
            player_two = p2;
            playing = true;
            Console.WriteLine("Two clients Connected");
            Thread player_one_th = new Thread(new ThreadStart(ReceiveCallBackPlayerOne));
            Thread player_two_th = new Thread(new ThreadStart(ReceiveCallBackPlayerTwo));
            player_one_th.Start();
            player_two_th.Start();
        }


        private void Send(Socket socket, Fonction fonction)
        {
            bool sent = false;
            int attempt = 0;
            while (!sent)
            {
                if (attempt == 10000)
                {
                    Console.WriteLine("Impossible d'envoyer une donnée.");
                    return;
                }
                try
                {
                    Stream stream = new NetworkStream(socket);
                    formatter.Serialize(stream, fonction);
                    //stream.Close();
                    sent = true;
                }
                catch (Exception)
                {
                    attempt++;
                    sent = false;
                }
            }
        }


        public void ReceiveCallBackPlayerTwo()
        {
            try { 
                Stream stream = new NetworkStream(player_two);
                Fonction fnc = (Fonction)formatter.Deserialize(stream);
                stream.Close();


                if (fnc.GetName().Equals("init"))
                {
                    bool place;
                    if (!init)
                    {
                        Random rand = new Random();
                        int r = rand.Next(0, 2);
                        if (r == 0)
                        {
                            placement = false;
                        }
                        else
                        {
                            placement = true;
                        }
                        init = true;
                        place = placement;
                    }
                    else
                    {
                        place = !placement;
                    }

                    List<object> paramSendTwo = new List<object>() { fnc.GetParam()[0], place , true };
                    List<object> paramSendOne = new List<object>() { fnc.GetParam()[0], place , false };
                    fnc = new Fonction("PlacementInit", paramSendOne);

                    new Thread(() => Send(player_two, new Fonction("PlacementInit", paramSendTwo))).Start();
                }
                else
                {
                    new Thread(() => Send(player_one, fnc)).Start();
                }
                ReceiveCallBackPlayerTwo();
            }
            catch (Exception)
            {
                Console.WriteLine("Player Two a perdu la connexion");
            }
}

        public void ReceiveCallBackPlayerOne()
        {
            try
            {
                Stream stream = new NetworkStream(player_one);
                Fonction fnc = (Fonction)formatter.Deserialize(stream);
                stream.Close();


                if (fnc.GetName().Equals("init"))
                {
                    bool place;
                    if (!init)
                    {
                        Random rand = new Random();
                        int r = rand.Next(0, 2);
                        if (r == 0)
                        {
                            placement = false;
                        }
                        else
                        {
                            placement = true;
                        }
                        init = true;
                        place = placement;
                    }
                    else
                    {
                        place = !placement;
                    }

                    List<object> paramSendTwo = new List<object>() { fnc.GetParam()[0], place, false };
                    List<object> paramSendOne = new List<object>() { fnc.GetParam()[0], place, true };
                    fnc = new Fonction("PlacementInit", paramSendTwo);

                    new Thread(() => Send(player_one, new Fonction("PlacementInit", paramSendOne))).Start();
                }
                new Thread(() => Send(player_two, fnc)).Start();

                ReceiveCallBackPlayerOne();
            }
            catch (Exception)
            {
                Console.WriteLine("Player One a perdu la connexion");
            }
        }
    }
}
