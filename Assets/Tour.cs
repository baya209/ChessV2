
using System.Collections.Generic;
using UnityEngine;

public class Tour : Piece
{
    
    public Tour(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur) 
    {
        setSymbole('T');
        setValeur(500);
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
    public override List<Coup> GenererCoupsPossibles(Plateau plateau)
    {
        List<Coup> coups = new List<Coup>();
        int[,] t = getTableau();
        int ligne = getLigne();
        int colonne = getColonne();
        int couleur = getCouleur();

        // 4 directions
        int[][] directions = new int[][]
        {
        new int[] { -1, 0 }, // haut
        new int[] { 1, 0 },  // bas
        new int[] { 0, -1 }, // gauche
        new int[] { 0, 1 }   // droite
        };

        foreach (int[] dir in directions)
        {
            int l = ligne + dir[0];
            int c = colonne + dir[1];

            while (l >= 0 && l < 8 && c >= 0 && c < 8)
            {
                int cible = t[l, c];

                if (cible == 0)
                {
                    // Case vide
                    coups.Add(new Coup(ligne, colonne, l, c, -1));
                }
                else if (cible != couleur)
                {
                    // Pièce ennemie
                    coups.Add(new Coup(ligne, colonne, l, c, cible));
                    break;
                }
                else
                {
                    // Pièce alliée 
                    break;
                }

                // Continue dans la même direction
                l += dir[0];
                c += dir[1];
            }
        }

        return coups;
    }
    public override Piece Cloner()
    {
        return new Tour((int[,])getTableau().Clone(), getLigne(), getColonne(), getCouleur());
    }
}
