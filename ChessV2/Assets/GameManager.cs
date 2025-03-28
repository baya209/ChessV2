using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text echiquier;
    Partie partie = new Partie();
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(partie.jouerCoup(0, 1, 0, 2, 1));
        echiquier.text = partie.afficher();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
