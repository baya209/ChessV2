using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ConnecteurClientV2 : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private string dernierMessageRecu;

    // Démarre la connexion et écoute les messages
    private void Start()
    {
        client = new TcpClient("127.0.0.1", 8080);
        stream = client.GetStream();
        Thread threadReception = new Thread(RecevoirMessages);
        threadReception.Start();
    }

    private void RecevoirMessages()
    {
        byte[] buffer = new byte[1024];
        while (true)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                lock (this)
                {
                    dernierMessageRecu = message; // stocké temporairement
                }
            }
        }
    }

    // Lire et CONSOMMER le message
    public string ConsommerDernierMessage()
    {
        lock (this)
        {
            string message = dernierMessageRecu;
            dernierMessageRecu = null; // suppression après lecture
            return message;
        }
    }

    // Envoyer un message
    public void EnvoyerMessage(string message)
    {
        if (stream != null)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }
}
