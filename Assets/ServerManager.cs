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

    /** Proposition de CHATGPT pour r�gler le probl�me de freeze en cr�ant un deuxi�me Threand
        * pour pouvoir g�rer la logique du serveur en parall�le. **/
     Thread serveurThread;

    public static void Main()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        // Proposition pour lancer le nouveau Threand dans l'arri�re plan 
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
            //Instancier le port d'entr�e du serveur 
            // Int32 --> 32 bits 
            Int32 port = 8080;
            // Permet de se connecter directeemnt � l'adresse IP sur lequel se trouve le pc h�te
            IPAddress adresseLocale = IPAddress.Parse("127.0.0.1");

            // Instancier le server en indiquant le port d'entr�e et l'adresse IP de la connexion
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