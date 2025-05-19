using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacerPiece : MonoBehaviour
{
    [SerializeField] private PlateuDeJeu plateuDeJeu;
    Vector3 vecteur3D;
    // Start is called before the first frame update
    void Start()
    {
       Debug.Log(transform.position);
        vecteur3D = plateuDeJeu.GetPlateauJeu()[7,7].transform.position;
        Vector3 posActuelle = transform.position;
        transform.position = new Vector3(vecteur3D.x , posActuelle.y ,vecteur3D.z);
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
