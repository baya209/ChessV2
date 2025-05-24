using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ServeurManagerV2 : MonoBehaviour
{
    TcpListener serveur;
    TcpClient client1, client2;
    NetworkStream stream1, stream2;
    Thread serveurThread;

    void Start()
    {
        serveurThread = new Thread(LancerServeur);
        serveurThread.IsBackground = true;
        serveurThread.Start();
    }

    void LancerServeur()
    {
        try
        {
            int port = 8080;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serveur = new TcpListener(ip, port);
            serveur.Start();
            Debug.Log(" Serveur lancé. En attente de connexions...");

            client1 = serveur.AcceptTcpClient();
            stream1 = client1.GetStream();
            Debug.Log(" Client1 connecté");

            client2 = serveur.AcceptTcpClient();
            stream2 = client2.GetStream();
            Debug.Log(" Client2 connecté");

            new Thread(() => EcouterClient(client1, stream2, "Client1")).Start();
            new Thread(() => EcouterClient(client2, stream1, "Client2")).Start();
        }
        catch (SocketException e)
        {
            Debug.LogError(" Erreur serveur : " + e.Message);
        }
    }

    void EcouterClient(TcpClient sourceClient, NetworkStream destinationStream, string nomClient)
    {
        try
        {
            NetworkStream sourceStream = sourceClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                Debug.Log($" {nomClient} a envoyé : {message}");

                byte[] dataToSend = System.Text.Encoding.UTF8.GetBytes(message + "\n");
                destinationStream.Write(dataToSend, 0, dataToSend.Length);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(" Erreur dans l'écoute de " + nomClient + " : " + e.Message);
        }
    }
}
