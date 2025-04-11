using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text echiquier;
    Partie partie = new Partie();
    public InputField fli;
    public string li;
    public string ci;
    public string lf;
    public string cf;
    public int ili;
    // Start is called before the first frame update
    void Start()
    {
        fli.onEndEdit.AddListener(OnInputEndEdit);
        Debug.Log(partie.jouerCoup(0, 1, 0, 2, 1));
        echiquier.text = partie.afficher();
    }
    public void OnInputEndEdit(string li)
    {
        ili = int.Parse(li);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
