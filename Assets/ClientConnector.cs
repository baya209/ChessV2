using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class ClientConnector : MonoBehaviour
{
    public TMP_InputField ipInput;     // Champ pour saisir l'adresse IP
    //public GameObject connectButton;   // Bouton "Se connecter"

    void Start()
    {
        if (ipInput == null)
        {
            Debug.LogError("Champ IP non assigné !" +gameObject.name
             );
            return;
        }

        ipInput.onSubmit.AddListener(delegate { ConnectToServer(); });

        NetworkManager.Singleton.OnClientConnectedCallback += (clientId) =>
        {
            Debug.Log("Connecté avec succès !");
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (clientId) =>
        {
            Debug.Log("Échec ou déconnexion !");
        };
        /*ipInput.onSubmit.AddListener(delegate { ConnectToServer(); });

        NetworkManager.Singleton.OnClientConnectedCallback += (clientId) =>
        {
            Debug.Log("Connecté avec succès !");
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (clientId) =>
        {
            Debug.Log("Échec ou déconnexion !");
        };*/
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
