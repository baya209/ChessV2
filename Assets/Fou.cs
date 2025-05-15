using System;
using System.Collections.Generic;
using UnityEngine;

public class Fou : Piece
{
    public Fou(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('F');
        setValeur(350);
    }

    public override bool deplacer(int l, int c)
    {
        //Déplacement en x et y
        int depx = l - getLigne();
        int depy = c - getColonne();

       
        // vérifie si la case et vide ou s'il y à un piont adverse
        if (getTableau()[l, c] == 0 || getTableau()[l, c] == (-1 * getCouleur()))
        {
            // vérifier s'il y a un déplacement en diagonale
            if (Math.Abs(depx) == Math.Abs(depy))
            {

                int directionLigne;
                int directionColonne;

                if (depx > 0)
                {
                    directionLigne = 1; // Se déplace vers le bas
                }
                else
                {
                    directionLigne = -1; // Se déplace vers le haut
                }
                

                if (depy > 0)
                {
                    directionColonne = 1; // droite
                }
                else
                {
                    directionColonne = -1; //gauche
                }
                

                int ligneActuelle = getLigne();  //initialiser la ligne actuelle 
                int colonneActuelle = getColonne(); // Initialise la colonne actuelle 

                while (ligneActuelle != l && colonneActuelle != c) // Parcourt les cases diagonales jusqu'à la position rentrée
                {
                    /** --- Pour tester les valeurs entrées ---
                    Debug.LogError(ligneActuelle);// ------
                    Debug.LogError(colonneActuelle);// -----
                    Debug.LogError(directionColonne);// -----
                    Debug.LogError(directionLigne);// -----
                    **/

                    //Incremente la position à vérifier case par case (en effectuant la première vérification à partir de la deuxième case) si non ca break directement de la méthode

                    ligneActuelle += directionLigne; // si cest vert le haut ça aditionne si c'est vers le bas ça soustrait
                    colonneActuelle += directionColonne; // si cest vert le haut ça aditionne si c'est vers le bas ça soustrait

                    if (getTableau()[ligneActuelle, colonneActuelle] == 0 || getTableau()[ligneActuelle, colonneActuelle] == (-1 * getCouleur())) // Vérifie si une pièce de même couleur bloque le passage
                    {
                        
                        return true;
                    }
                   

                }
                Debug.LogError("Deplacement invalide");
                return false;
            }
            else
            {
                Debug.LogError("Trajectoire non valide pour le Fou");
            }
        }
        Debug.LogError("Une pièce de même couleur se trouve sur le trajet");
        return false;
    }

    public override bool[,] isDanger(bool[,] danger)
    {
        return danger;
    }
    public override List<Coup> GenererCoupsPossibles(Plateau plateau)
    {
        List<Coup> coups = new List<Coup>();
        int[,] t = getTableau();
        int ligne = getLigne();
        int colonne = getColonne();
        int couleur = getCouleur();

        //  diagonales
        int[][] directions = new int[][]
        {
        new int[] { 1, 1 },   // bas droite
        new int[] { 1, -1 },  // bas gauche
        new int[] { -1, 1 },  // haut droite
        new int[] { -1, -1 }  // haut gauche
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
                    // Pièce ennemie = capture
                    coups.Add(new Coup(ligne, colonne, l, c, cible));
                    break;
                }
                else
                {
                    // Pièce alliée = pas passer
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
        Fou clone = new Fou((int[,])getTableau().Clone(), getLigne(), getColonne(), getCouleur());
        clone.setValeur(this.getValeur());
        if (!this.isFixe()) clone.setFixe();
        clone.setSymbole(this.getSymbole());
        if (this.isEchec() != null) clone.setEchec((bool[,])this.isEchec().Clone());
        return clone;
    }
    /*
    public override Piece Cloner()
    {
        return new Fou((int[,])getTableau().Clone(), getLigne(), getColonne(), getCouleur());
    }*/
}
