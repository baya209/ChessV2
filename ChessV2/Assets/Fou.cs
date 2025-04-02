using System;
using UnityEngine;

public class Fou : Piece
{
    public Fou(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('F');
    }

    public override bool deplacer(int l, int c)
    {
        // vérifie si la case et vide 
        if (getTableau()[l, c] == 0 || getTableau()[l, c] == (-1 * getCouleur()))
        {
            // vérifier s'il y a un déplacement en diagonale
            if (Math.Abs(l - getLigne()) == Math.Abs(c - getColonne()))
            {

                int directionLigne;
                if (l > getLigne())
                {
                    directionLigne = 1; // Se déplace vers le bas
                }
                else
                {
                    directionLigne = -1; // Se déplace vers le haut
                }
                int directionColonne;

                if (c > getColonne())
                {
                    directionColonne = 1; // droite

                }
                else
                {
                    directionColonne = -1; //gauche
                }


                int ligneActuelle = getLigne() + directionLigne; //initialiser la ligne actuelle 
                int colonneActuelle = getColonne() + directionColonne; // Initialise la colonne actuelle 

                while (ligneActuelle != l && colonneActuelle != c) // Parcourt les cases diagonales jusqu'à la position rentrée
                {
                    if (getTableau()[ligneActuelle, colonneActuelle] != 0) // Vérifie si une pièce bloque le passage
                    {
                        return false;
                    }
                    ligneActuelle += directionLigne; // si cest vert le haut ça aditionne si c'est vers le bas ça soustrait
                    colonneActuelle += directionColonne; // si cest vert le haut ça aditionne si c'est vers le bas ça soustrait

                }
                return true;
            }
            else
            {
                Debug.LogError("Trajectoire non valide pour le Fou");
            }
        }
        return false;
    }

    public override bool[,] isDanger(bool[,] danger)
    {
        return danger;
    }

}
