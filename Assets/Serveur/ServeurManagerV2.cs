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

    /// <summary>
    /// Instancie le serveur avec l'iP de l'hote et d'un port quelconque et accepte l'entr�e de deux utilisateurs (cleints)
    /// </summary>
    void LancerServeur()
    {
        try
        {
            int port = 8080;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serveur = new TcpListener(ip, port);
            serveur.Start();
            Debug.Log(" Serveur lanc�. En attente de connexions...");

            client1 = serveur.AcceptTcpClient();
            stream1 = client1.GetStream();
            Debug.Log(" Client1 connect�");

            client2 = serveur.AcceptTcpClient();
            stream2 = client2.GetStream();
            Debug.Log(" Client2 connect�");

            new Thread(() => EcouterClient(client1, stream2, "Client1")).Start();
            new Thread(() => EcouterClient(client2, stream1, "Client2")).Start();
        }
        catch (SocketException e)
        {
            Debug.LogError(" Erreur serveur : " + e.Message);
        }
    }

    /// <summary>
    /// Attend que l'un des clients envoie un message, le d�code et le renvoie � l'autre client
    /// </summary>
    /// <param name="sourceClient"></param>
    /// <param name="destinationStream"></param>
    /// <param name="nomClient"></param>
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
                Debug.Log($" {nomClient} a envoy� : {message}");

                byte[] dataAEnvoyer = System.Text.Encoding.UTF8.GetBytes(message + "\n");
                destinationStream.Write(dataAEnvoyer, 0, dataAEnvoyer.Length);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(" Erreur dans l'�coute de " + nomClient + " : " + e.Message);
        }
    }
}
