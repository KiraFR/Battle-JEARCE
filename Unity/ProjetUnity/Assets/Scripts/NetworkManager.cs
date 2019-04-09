using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public int port = 8181;
    public string ip;

    private readonly Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private IPAddress iPAddress;
    private Thread receivedThread;
    private Thread mainThread;
    private List<string> queueCommand = new List<string>();


    void Start()
    {
        iPAddress = IPAddress.Parse(ip);
        ConnectToServer();
        mainThread = Thread.CurrentThread;
        receivedThread = new Thread(new ThreadStart(Receive));
        receivedThread.Start();
    }

    private void ConnectToServer()
    {
        int attempts = 0;

        while (!ClientSocket.Connected)
        {
            try
            {
                attempts++;
                Debug.Log("Connection attempt " + attempts);
                ClientSocket.Connect(iPAddress, port);
            }
            catch (SocketException)
            {
                Debug.ClearDeveloperConsole();
            }
        }

        Debug.ClearDeveloperConsole();
        Debug.Log("Connected");
    }

    private void Exit()
    {
        SendString("exit");
        ClientSocket.Shutdown(SocketShutdown.Both);
        ClientSocket.Close();
        receivedThread.Abort();
    }

    private void SendString(string text)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(text);
        ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
    }
    
    void Update()
    {
        if(queueCommand.Count > 0)
        {
            for(int i = 0; i < queueCommand.Count; i++)
            {
                string command = queueCommand[i];
                string[] list = command.Split(' ');

                if (list.Length > 0)
                {
                    int xStart, yStart, xFin, yFin;
                    if (list[0] == "move")
                    {
                        int.TryParse(list[1], out xStart);
                        int.TryParse(list[2], out yStart);
                        int.TryParse(list[3], out xFin);
                        int.TryParse(list[4], out yFin);
                        GameObject square = GameManager.instance.GetGameObject(xStart, yStart);
                        GameManager.instance.Deplacement(xStart, yStart, xFin, yFin);
                        List<Vector3> chemin = GameManager.instance.GetChemin();
                        if (square != null)
                        {
                            Character character = square.GetComponent<Square>().GetCharacter();
                            if (character != null)
                            {
                                GameManager.instance.MoveCharacter(character, chemin);
                            }
                        }

                    }
                }
                queueCommand.RemoveAt(i);
            }
        }
    }

    private void Receive()
    {
        var buffer = new byte[2048];
        int received = ClientSocket.Receive(buffer, SocketFlags.None);
        if (received > 0)
        {
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string receivedData = Encoding.ASCII.GetString(data);
            if(receivedData != "")
            {
                queueCommand.Add(receivedData);
            }
        }
        Receive();
    }

}
