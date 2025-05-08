using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cavalier : Piece
{
    //possibilite des mouvements (8 directions)
    int[,] mouvements = {
            { -2, -1 }, { -2, 1 }, { -1, -2 }, { -1, 2 },
            { 1, -2 }, { 1, 2 }, { 2, -1 }, { 2, 1 }
        };
    public Cavalier(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('C');
    }

    public override bool deplacer(int l, int c)
    {

        int depx = l - getLigne();
        int depy = c - getColonne();

        int[,] deplacement = { { depx, depy } };




        //verifie mouvement L
        if (tableauxIdentiques(mouvements, deplacement)){           

            //verifier pos occuper                                                                                  
            if (getTableau()[l, c] == 0)
            {
                return true;
            }

            else if ((getTableau()[l, c] == (-1 * getCouleur()))) {              
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.LogError("Deplacement invalide"); 
        }
        return false;    
        
    }


    public override bool[,] isDanger(bool[,] danger)
    {
        


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

    //Methode pour vérifier si des coordonées sont égales (vecteurs identiques)
    public bool tableauxIdentiques(int[,]ensembleDeplacement, int[,]deplacementEntre)
    {
        for (int i = 0; i < mouvements.GetLength(0); i++)
        {
            for (int j = 0; j < mouvements.GetLength(1); j++)
            {
                if (ensembleDeplacement[i, j] == deplacementEntre[0,0])
                {
                    return true;
                }
            }
            
        }
        return false;
    }

    public override List<Coup> GenererCoupsPossibles(Plateau plateau)
    {
        List<Coup> coups = new List<Coup>();
        int[,] t = getTableau();
        int ligne = getLigne();
        int colonne = getColonne();
        int couleur = getCouleur();

        // 8 positions possibles en "L"
        int[][] mouvements = new int[][]
        {
        new int[] { 2, 1 },
        new int[] { 2, -1 },
        new int[] { -2, 1 },
        new int[] { -2, -1 },
        new int[] { 1, 2 },
        new int[] { 1, -2 },
        new int[] { -1, 2 },
        new int[] { -1, -2 }
        };

        foreach (int[] m in mouvements)
        {
            int l = ligne + m[0];
            int c = colonne + m[1];

            if (l >= 0 && l < 8 && c >= 0 && c < 8)
            {
                int cible = t[l, c];

                if (cible == 0 || cible != couleur)
                {
                    int pieceCapturee = (cible == 0) ? -1 : cible;
                    coups.Add(new Coup(ligne, colonne, l, c, pieceCapturee));
                }
            }
        }

        return coups;
    }
    public override Piece Cloner()
    {
        return new Cavalier((int[,])getTableau().Clone(), getLigne(), getColonne(), getCouleur());
    }
}
