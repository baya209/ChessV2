using System;
using UnityEngine;
public class Roi : Piece
{
    
    public Roi(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('r');
    }
    public override bool deplacer(int l, int c)
    {
        if (l >= 0 && l < 8 && c >= 0 && c < 8)
        {
            if (getCouleur() != getTableau()[l, c] && (l != getLigne() || c != getColonne()))
            {
                if (!isEchec()[l, c])
                {
                    if (Math.Abs(l - getLigne()) <= 1 && Math.Abs(c - getColonne()) <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.LogError("Plus de d'une case");
                    }
                }
                else
                {
                    Debug.LogError("Le roi est en echec");
                }
            }
            else
            {
                Debug.LogError("Position occuppe");
            }
    
        }
        
        return false;

    }
    

    public override bool[,] isDanger(bool[,] danger)
    {
        if (getLigne() > 0)
        {
            if (getColonne() > 0)
            {
                danger[getLigne() - 1, getColonne() - 1] = true;
            }
            if (getColonne() < 6)
            {
                danger[getLigne() - 1, getColonne() + 1] = true;
            }
            danger[getLigne() - 1, getColonne()] = true;
        }
        if (getLigne() < 6)
        {
            if (getColonne() > 0)
            {
                //danger[getLigne() +1, getColonne() - 1] = true;
            }
            if (getColonne() < 6)
            {
                
                danger[getLigne() + 1, getColonne() + 1] = true;
            }
            danger[getLigne() + 1, getColonne()] = true;
        }
        if(getColonne() < 6)
        {
            danger[getLigne(), getColonne()+1] = true;
        }
        if (getColonne() > 0)
        {
            danger[getLigne(), getColonne() -1] = true;
        }

        return danger;
    }
}
