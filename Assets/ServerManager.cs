using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Pour le serveur LAN 
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ServerManager : MonoBehaviour
{
    TcpListener serveur = null;

    /** Proposition de CHATGPT pour régler le problème de freeze en créant un deuxième Threand
        * pour pouvoir gérer la logique du serveur en parallèle. **/
     Thread serveurThread;

    public static void Main()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        // Proposition pour lancer le nouveau Threand dans l'arrière plan 
        serveurThread = new Thread(lancerServeur);
        serveurThread.IsBackground = true;
        serveurThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void lancerServeur()
    {
        try
        {
            //Instancier le port d'entrée du serveur 
            // Int32 --> 32 bits 
            Int32 port = 8080;
            // Permet de se connecter directeemnt à l'adresse IP sur lequel se trouve le pc hôte
            IPAddress adresseLocale = IPAddress.Parse("127.0.0.1");

            // Instancier le server en indiquant le port d'entrée et l'adresse IP de la connexion
            serveur = new TcpListener(adresseLocale, port);

            serveur.Start();

            if (serveurThread != null)
            {
                Debug.Log("Attente connexion...");
            }
            
            TcpClient client1 = serveur.AcceptTcpClient();
            TcpClient client2 = serveur.AcceptTcpClient();
          
            if(client1 != null) 
                Debug.Log("Client1 Connected!");

            if(client2 != null)
                Debug.Log("Client2 Connected!");
          
            

        }
        catch (SocketException e)
        {
            Console.WriteLine(e.ToString());
            Debug.Log("Erreur de serveur");
            serveur.Stop();

        }
        finally
        {
            
            
        }

    }
}