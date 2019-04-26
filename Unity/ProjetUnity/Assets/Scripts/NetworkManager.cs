using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using testDLL;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public int port = 8181;
    public string ip;
    private readonly Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private IPAddress iPAddress;
    private Thread receivedThread;
    private List<Fonction> queueCommand = new List<Fonction>();
    private bool stopped;

    void Start()
    {
        stopped = false;
    }

    public void StartConnection()
    {
        iPAddress = IPAddress.Parse(ip);
        new Thread(new ThreadStart(ConnectToServer)).Start();
        receivedThread = new Thread(new ThreadStart(Receive));
        receivedThread.Start();
    }

    void ConnectToServer()
    {
        int attempts = 0;

        while (!ClientSocket.Connected)
        {
            try
            {
                attempts++;
                ClientSocket.Connect(iPAddress, port);
            }
            catch (SocketException)
            {
                if (attempts == 50)
                {
                    stopped = true;
                }
            }
        }

    }

    void OnDestroy()
    {
        SendString("exit",new List<object>());
        ClientSocket.Shutdown(SocketShutdown.Both);
        ClientSocket.Close();
        receivedThread.Abort();
    }

    public void SendString(string text,List<object> param)
    {
        try { 
            Stream stream = new NetworkStream(ClientSocket);
            BinaryFormatter bin = new BinaryFormatter();
            bin.Serialize(stream, new Fonction(text, param));
            //stream.Close();
        }
        catch (Exception)
        {
            ClientSocket.Close();
            stopped = true;
        }
    }
    
    void FixedUpdate()
    {
        if (stopped)
        {

        }else if(queueCommand.Count > 0)
        {
            for(int i = 0; i < queueCommand.Count; i++)
            {
                Fonction command = queueCommand[i];
                Type type = typeof(GameManager);
                MethodInfo method = type.GetMethod(command.GetName());
                Debug.Log(method);

                if (method != null)
                {
                    method.Invoke(GameManager.instance, command.GetParam().ToArray());
                }
                queueCommand.RemoveAt(i);
            }
        }
    }

    private void Receive()
    {
        try
        {
            Stream stream = new NetworkStream(ClientSocket);
            BinaryFormatter bin = new BinaryFormatter();
            Fonction fnc = (Fonction)bin.Deserialize(stream);
            queueCommand.Add(fnc);
            //stream.Close();
            Receive();
        }
        catch (Exception)
        {
            ClientSocket.Close();
            stopped = true;
        }
    }
}
