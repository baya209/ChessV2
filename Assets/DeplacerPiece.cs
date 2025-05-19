using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacerPiece : MonoBehaviour
{
    [SerializeField] private PlateuDeJeu plateuDeJeu;
    public float posX;
    public float posY;
    Vector3 vecteur3D;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = plateuDeJeu.GetCentreTuile(posX,posY);
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
