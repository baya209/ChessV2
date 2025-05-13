using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case
{
    private int posX;
    private int posY;
    //private bool estOccupe = false;
    [SerializeField] public Tuile tuile; 

    public Case(int posX, int posY)
    {
        posX = posX;
        posY = posY;


    }

}

