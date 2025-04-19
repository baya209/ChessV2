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

}
