
using UnityEngine;

public class Tour : Piece
{
    
    public Tour(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur) 
    {
        setSymbole('t');
    }
    public override bool deplacer(int l, int c)
    {
        if(getTableau()[l, c] == 0 || getTableau()[l, c] == (-1*getCouleur()))//Verifie que la case est vide 
        {
            if (c != getColonne() && l == getLigne())
            {
                if (c > getColonne())
                {
                    for (int i = getColonne()+1; i <= c; i++)
                    {
                        if (getTableau()[i, l] != 0)
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
                        if (getTableau()[i, l] != 0)
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
                    for (int i = getLigne()+1; i <= l; i++)
                    {
                        if (getTableau()[c, i] != 0)
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
                        if (getTableau()[c, i] != 0)
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
            danger[i, getLigne()] = true;
            if (getTableau()[i, getLigne()] != 0)
            {
                break;
            }
            danger[i, getLigne()] = true;
        }
        for (int i = getColonne()-1; i >= getColonne()-1; i--)
        {
            danger[i, getLigne()] = true;
            // c'est pas sensé etre ligne apres ça colonne???
            if (getTableau()[i, getLigne()] != 0)
            {
                break;
            }
        }
        for (int i = getLigne()+1; i < 8 - getLigne(); i++)
        {
            danger[getColonne(), i] = true;
            if (getTableau()[getColonne(), i] != 0)
            {   
                break;
            }
        }
        for (int i = getLigne() - 1; i >= getLigne()-1; i--)
        {
            danger[getColonne(), i] = true;
            if (getTableau()[getColonne(), i] != 0)
            {
                break;
            }
        }
        return danger;
    }
}
