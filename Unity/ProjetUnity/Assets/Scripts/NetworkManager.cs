using SerialFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public int port = 8181;
    public string ip;
    
    private readonly Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private IPAddress iPAddress;
    private Thread receivedThread;
    private List<SerialClass> queueCommand = new List<SerialClass>();
    private bool stopped;
    private Stream stream;
    private BinaryFormatter formatter;

    void Start()
    {
        stopped = false;
    }

    public async Task StartConnection()
    {
        formatter = new BinaryFormatter();
        iPAddress = IPAddress.Parse(ip);
        bool connected = await ConnectToServer();
        if (connected)
        {
            receivedThread = new Thread(new ThreadStart(Receive));
            receivedThread.Start();
        }
    }

    public async Task<bool> ConnectToServer()
    {
        await ClientSocket.ConnectAsync(iPAddress, port);
        stream = new NetworkStream(ClientSocket);

        return ClientSocket.Connected;
    }

    void OnDestroy()
    {
        SendString("exit",new List<object>());
        ClientSocket.Shutdown(SocketShutdown.Both);
        ClientSocket.Close();
        receivedThread.Abort();
    }

    public void Disconnect()
    {
        ClientSocket.Shutdown(SocketShutdown.Both);
        ClientSocket.Close();
        receivedThread.Abort();
    }

    public void SendString(string text,List<object> param)
    {
        try { 
            formatter.Serialize(stream, new SerialClass(text, param));
        }
        catch (Exception)
        {
            //ClientSocket.Close();
            //stream.Close();
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
                SerialClass command = queueCommand[i];
                Type type = typeof(GameManager);
                MethodInfo method = type.GetMethod(command.GetName());
                object[] param = command.GetParam().ToArray();

                if (method != null)
                {
                    method.Invoke(GameManager.instance, param);
                }
                queueCommand.RemoveAt(i);
            }
        }
    }

    private void Receive()
    {
        if (ClientSocket.Connected)
        {
            SerialClass fnc = (SerialClass)formatter.Deserialize(stream);
            queueCommand.Add(fnc);
        }
        Receive();
    }
}
