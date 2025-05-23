using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using TMPro;
public class hoteManager : MonoBehaviour
{
 


    public TMP_Text ipText;         // Texte pour afficher l'IP locale
    public TMP_Text statusText;     // Texte "en attente..."
    public string sceneNom = "Plateau"; // À remplacer

    void Start()
    {
        string localIP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            .AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();

        ipText.text = "Votre IP locale : " + localIP;
        statusText.text = "En attente d'un joueur...";

        // Démarre l'hôte+
        NetworkManager.Singleton.StartHost();

        // Optionnel : écouter les connexions
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.ConnectedClientsList.Count >= 2)
        {
            statusText.text = "Joueur connecté !";

            // Charge la scène de jeu (si besoin)
            NetworkManager.Singleton.SceneManager.LoadScene(sceneNom, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}


