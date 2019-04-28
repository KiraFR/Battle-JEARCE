using SerialFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

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
        Stream player_one_stream;
        Stream player_two_stream;
        private bool player_one_ready;
        private bool player_two_ready;


        public Game(Socket p1, Socket p2)
        {
            player_one = p1;
            player_two = p2;
            player_one_stream = new NetworkStream(player_one);
            player_two_stream = new NetworkStream(player_two);
            playing = true;
            player_one_ready = false;
            player_two_ready = false;
            Console.WriteLine("Two clients Connected");
            Thread player_one_th = new Thread(new ThreadStart(ReceiveCallBackPlayerOne));
            Thread player_two_th = new Thread(new ThreadStart(ReceiveCallBackPlayerTwo));
            player_one_th.Start();
            player_two_th.Start();
        }
        
        private void Send(Stream socket, SerialClass fonction)
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
                    formatter.Serialize(socket, fonction);
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
                SerialClass query = (SerialClass)formatter.Deserialize(player_two_stream);

                if (query.GetName().Equals("init"))
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

                    List<object> paramSendTwo = new List<object>() { query.GetParam()[0], place , true };
                    new Thread(() => Send(player_two_stream, new SerialClass("PlacementInit", paramSendTwo))).Start();


                    List<object> paramSendOne = new List<object>() { query.GetParam()[0], place , false };
                    query = new SerialClass("PlacementInit", paramSendOne);
                }
                else if(query.GetName().Equals("ready"))
                {
                    player_two_ready = true;

                    if (player_one_ready && player_two_ready)
                    {
                        bool start = (new Random().Next(0, 2) % 2) == 0;
                        new Thread(() => Send(player_two_stream, new SerialClass("StartGame", new List<object>() { !start }))).Start();

                        query = new SerialClass("StartGame", new List<object>() { start });
                    }

                }

                new Thread(() => Send(player_one_stream, query)).Start();
                ReceiveCallBackPlayerTwo();
            }
            catch (Exception e)
            {
                Console.WriteLine("Player Two a perdu la connexion : " + e.Message);
                formatter.Serialize(player_one_stream, new SerialClass("Surrended", new List<object>()));
            }
        }

        public void ReceiveCallBackPlayerOne()
        {
            try
            {
                SerialClass query = (SerialClass)formatter.Deserialize(player_one_stream);

                if (query.GetName().Equals("init"))
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

                    
                    List<object> paramSendOne = new List<object>() { query.GetParam()[0], place, true };
                    new Thread(() => Send(player_one_stream, new SerialClass("PlacementInit", paramSendOne))).Start();

                    
                    List<object> paramSendTwo = new List<object>() { query.GetParam()[0], place, false };
                    query = new SerialClass("PlacementInit", paramSendTwo);

                }
                else if (query.GetName().Equals("ready"))
                {
                    player_one_ready = true;

                    if (player_one_ready && player_two_ready)
                    {
                        bool start = (new Random().Next(0, 2) % 2) == 0;
                        new Thread(() => Send(player_one_stream, new SerialClass("StartGame", new List<object>() { !start }))).Start();

                        query = new SerialClass("StartGame", new List<object>() { start });
                    }
                }
                new Thread(() => Send(player_two_stream, query)).Start();

                ReceiveCallBackPlayerOne();
            }
            catch (Exception e)
            {
                Console.WriteLine("Player One a perdu la connexion : " + e.Message);
                formatter.Serialize(player_two_stream, new SerialClass("Surrended", new List<object>()));
            }
        }
    }
}
