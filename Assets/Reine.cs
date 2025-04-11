using System;
using UnityEngine;

public class Reine : Piece
{
    public Reine(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('D');
    }

    public override bool deplacer(int l, int c) 
    {
        if (getTableau()[l, c] == 0 || getTableau()[l, c] == (-1 * getCouleur()))
        {
            if (c != getColonne() && l == getLigne())
            {
                if (c > getColonne()) 
                {
                    for (int i = getColonne() + 1; i <= c; i++) 
                    {
                        if (getTableau()[i, l] != 0) 
                        {
                            return false;
                        }
                    }
                    return true; 
                }
                else 
                {
                    for (int i = c - 1; i > getColonne(); i--) 
                    {
                        if (getTableau()[i, l] != 0) 
                        {
                            return false;
                        }
                    }
                    return true; 
                }
            }

            else if (l != getLigne() && c == getColonne())
            {
                if (l > getLigne()) // Déplacement vers le bas
                {
                    for (int i = getLigne() + 1; i <= l; i++) // Vérifie toutes les cases entre la position actuelle et la cible
                    {
                        if (getTableau()[c, i] != 0) // Si une pièce bloque le passage
                        {
                            return false;
                        }
                    }
                    return true; // Le chemin est libre, déplacement possible
                }
                else // Déplacement vers le haut
                {
                    for (int i = l - 1; i > getLigne(); i--) // Vérifie toutes les cases entre la position actuelle et la cible
                    {
                        if (getTableau()[c, i] != 0) // Si une pièce bloque le passage
                        {
                            return false;
                        }
                    }
                    return true; // Le chemin est libre, déplacement possible
                }
            }

            // Vérifie un déplacement diagonal

            else if (Math.Abs(l - getLigne()) == Math.Abs(c - getColonne()))
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
                Debug.LogError("Trajectoire non valide pour la reine");
            }

        }
        else
        {
            Debug.LogError("Position occupée"); 
        }
        return false; 
    }


    public override bool[,] isDanger(bool[,] danger)
    {
        return danger;
    }

}
