using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Profondeur: MonoBehaviour

{
    public TMP_Dropdown dropdown; //le boutton
   // public int profondeurMax = 3;

   public void ChangerProfondeur(int index)
    {

        switch (index)
        {
            case 0:
                NewMonoBehaviourScript.profondeurMax = 1;
                break; //changer profondeur 
            case 1:
                NewMonoBehaviourScript.profondeurMax = 2;
                break;
            case 2:
                NewMonoBehaviourScript.profondeurMax = 3;
                break;
        }
        Debug.Log("Profondeur chang�e � : " + NewMonoBehaviourScript.profondeurMax); // Afficher la nouvelle profondeur
    }
    private void Start()
    {
        // l'�v�nement OnValueChanged du dropdown
        dropdown.onValueChanged.AddListener(ChangerProfondeur);
    }


}
