using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de gérer si on crée un serveur ou un nouveau client
/// </summary>
public class GestionnaireAccueilServeur : MonoBehaviour
{
    public GameObject serveurPrefab;
    public GameObject clientPrefab;


    public void LancerServeur()
    {
        GameObject serveur = Instantiate(serveurPrefab);
        DontDestroyOnLoad(serveur);
    }

    public void RejoindreServeur()
    {
        GameObject client = Instantiate(clientPrefab);
        DontDestroyOnLoad(client);
    }

}
