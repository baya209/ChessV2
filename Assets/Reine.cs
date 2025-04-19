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
        //Déplacement en x et y
        int depx = l - getLigne();
        int depy = c - getColonne();

                    

        // vérifie si la case et vide ou s'il y à un piont adverse
        if (getTableau()[l, c] == 0 || getTableau()[l, c] == (-1 * getCouleur()))
        {                
            
                            //Deplacement comme le fou 

            // vérifier s'il y a un déplacement en diagonale (deplacement fou)
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
            else if (getTableau()[l, c] == 0 || getTableau()[l, c] == (-1 * getCouleur()))//Verifie que la case est vide 
            {
                if (c != getColonne() && l == getLigne())
                {
                    if (c > getColonne())
                    {
                        for (int i = getColonne() + 1; i < c; i++)
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
                        for (int i = c - 1; i > getColonne(); i--)
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
                        for (int i = getLigne() + 1; i < l; i++)
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
                        for (int i = l - 1; i > getLigne(); i--)
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
                    return false;
                }

            }






            else
            {
                Debug.LogError("Trajectoire non valide pour le Fou");
                return false;
            }
            return false;
        }



        /** 
         * ------ Ancien code --------

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
        **/
        return false;
    }


    public override bool[,] isDanger(bool[,] danger)
    {
        return danger;
    }

}
