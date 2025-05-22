using System;
using System.Net.Sockets;
using UnityEngine;

public class ClientConnector : MonoBehaviour
{
    private TcpClient client;

    void Start()
    {
        ConnectionAuServer();
    }

    void ConnectionAuServer()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 8080);
            Debug.Log(" Connected to server!");
            
        }
        catch (Exception ex)
        {
            Debug.LogError(" Failed to connect to server: " + ex.Message);
        }
    }

    private void OnApplicationQuit()
    {
        if (client != null && client.Connected)
        {
            client.Close();
            Debug.Log(" Disconnected from server.");
        }
    }
}