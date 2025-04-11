using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavalier : Piece
{
    public Cavalier(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('c');
    }

    public override bool deplacer(int l, int c)
    {

        int depx = l;
        int depy = c;




        //verifie mouvement L
        if((depx == 2 && depy == 1)|| (depx == 1 && depy == 2))


        {
            //verifier pos occuper
            if (getTableau()[l,c]==0|| getTableau()[l, c] == (-1 * getCouleur()))
            {
                return true;
            }
            
      
            else
            {
                Debug.LogError("Position occupée par une pièce de meme couleur");
            }
            return false;
        }
       

        else
                {
            Debug.LogError("Deplacement invalide");
        }
        return false;
    }

    public override bool[,] isDanger(bool[,] danger)
    {
        //possibilite des mouvements (8 directions)
        int[,] mouvements = {
            { -2, -1 }, { -2, 1 }, { -1, -2 }, { -1, 2 },
            { 1, -2 }, { 1, 2 }, { 2, -1 }, { 2, 1 }
        };


        //parcour chaque directions
        for (int i = 0; i < mouvements.GetLength(0); i++)
        {
            //nouvelle position apres le coup
            int nouveauL = getLigne() + mouvements[i, 0];
            int nouveauC = getColonne() + mouvements[i, 1];
            
            //limite du plateau
            if (nouveauL >= 0 && nouveauL < 8 && nouveauC >= 0 && nouveauC < 8)
            {
                danger[nouveauL, nouveauC] = true;
            }
        }
        return danger; // retourn le tebleau


    }
}
