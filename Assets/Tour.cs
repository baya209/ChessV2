
using UnityEngine;

public class Tour : Piece
{
    
    public Tour(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur) 
    {
        setSymbole('T');
    }
    public override bool deplacer(int l, int c)
    {
        if(getTableau()[l, c] == 0 || getTableau()[l, c] == (-1*getCouleur()))//Verifie que la case est vide 
        {
            if (c != getColonne() && l == getLigne())
            {
                if (c > getColonne())
                {
                    for (int i = getColonne()+1; i < c; i++)
                    {
                        if (getTableau()[l, i] != 0)
                        {
                            Debug.LogError("Piece sur la trajectoire");
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    for (int i = c-1; i > getColonne(); i--)
                    {
                        if (getTableau()[l, i] != 0)
                        {
                            Debug.LogError("Piece sur la trajectoire");
                            return false;
                        }
                    }
                    return true;
                }

            }
            else if (l != getLigne() && c == getColonne())
            {
                if (l > getLigne())
                {
                    for (int i = getLigne()+1; i < l; i++)
                    {
                        Debug.Log(getTableau()[i, c]);
                        if (getTableau()[i, c] != 0)
                        {
                            
                            Debug.LogError("Piece sur la trajectoire");
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    for (int i = l-1; i > getLigne(); i--)
                    {
                        if (getTableau()[i, c] != 0)
                        {
                            Debug.LogError("Piece sur la trajectoire");
                            return false;
                        }
                    }
                    return true;
                }
            }
            else
            {
                Debug.LogError("Trajectoire non lineaire");
            }
        }
        else
        {
            Debug.LogError("Position occupe"); 
        }
        return false;
    }

    public override bool[,] isDanger(bool[,] danger)
    {
        for (int i = getColonne()+1; i < 8 - getColonne(); i++) {
            danger[getLigne(),i] = true;
            if (getTableau()[i, getLigne()] != 0)
            {
                break;
            }
            danger[getLigne(), i] = true;
        }
        for (int i = getColonne()-1; i >= 0; i--)
        {
            danger[getLigne(), i] = true;
            // c'est pas sensé etre ligne apres ça colonne???
            if (getTableau()[getLigne(), i] != 0)
            {
                break;
            }
        }
        for (int i = getLigne()+1; i < 8 - getLigne(); i++)
        {
            danger[i,getColonne()] = true;
            if (getTableau()[i, getColonne()] != 0)
            {   
                break;
            }
        }
        for (int i = getLigne() - 1; i >= 0; i--)
        {
            danger[i, getColonne()] = true;
            if (getTableau()[i, getColonne()] != 0)
            {
                break;
            }
        }
        return danger;
    }
}
