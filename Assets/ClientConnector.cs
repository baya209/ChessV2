using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class ClientConnector : MonoBehaviour
{
    public TMP_InputField ipInput;     // Champ pour saisir l'adresse IP
    //public GameObject connectButton;   // Bouton "Se connecter"

    void Start()
    {//test
     // Vérifie qu’un bouton est assigné
        /*if (connectButton != null)
         {*/
        // GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ConnectToServer);
        /*}
         else
             Debug.LogError("Le bouton n’est pas assigné dans l’Inspector !");*/
        ipInput.onSubmit.AddListener(delegate { ConnectToServer(); });

    }

    void ConnectToServer()
    {
        string ip = ipInput.text;

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData(ip, 7777); // Port réseau par défaut

        NetworkManager.Singleton.StartClient();

       // Debug.Log("Tentative de connexion au serveur : " + ip);
        Debug.Log("Bouton cliqué ! IP : " + ip);
    }
}
