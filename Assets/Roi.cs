using System;
using System.Collections.Generic;
using UnityEngine;
public class Roi : Piece
{
    
    public Roi(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('R');
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

    public override List<Coup> GenererCoupsPossibles(Plateau plateau)
    {
        // Liste pour stocker tous les coups possibles du roi
        List<Coup> coups = new List<Coup>();

        // Récupère l'état du plateau, la position actuelle du roi et sa couleur
        int[,] t = getTableau();
        int ligne = getLigne();
        int colonne = getColonne();
        int couleur = getCouleur();

        // Parcourt toutes les directions possibles autour du roi (8 cases autour)
        for (int dl = -1; dl <= 1; dl++)
        {
            for (int dc = -1; dc <= 1; dc++)
            {
                if (dl == 0 && dc == 0) continue; // Ignore la case actuelle (ne pas rester sur place)

                int l = ligne + dl; // ligne cible
                int c = colonne + dc; // colonne cible

                // Vérifie que la case cible est dans les limites de l'échiquier
                if (l >= 0 && l < 8 && c >= 0 && c < 8)
                {
                    int cible = t[l, c]; // Valeur de la case cible : 0 = vide, 1 ou -1 = pièce

                    // Le roi peut se déplacer si :
                    // - la case est vide (cible == 0)
                    // - ou contient une pièce ennemie (cible != couleur)
                    if (cible == 0 || cible != couleur)
                    {
                        // Si la case est vide, on indique qu'il n'y a pas de pièce capturée
                        // Sinon, on garde la valeur de la pièce capturée
                        int pieceCapturee = (cible == 0) ? -1 : cible;

                        // Ajoute le coup à la liste
                        coups.Add(new Coup(ligne, colonne, l, c, pieceCapturee));
                    }
                }
            }
        }

        return coups;
    }


}
