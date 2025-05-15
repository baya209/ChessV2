using System;
using System.Collections.Generic;
using UnityEngine;

public class Reine : Piece
{
    public Reine(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('D');
        setValeur(1000);
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


    public override List<Coup> GenererCoupsPossibles(Plateau plateau)
    {
        List<Coup> coups = new List<Coup>();
        int[,] t = getTableau();
        int ligne = getLigne();
        int colonne = getColonne();
        int couleur = getCouleur();

        //  8 directions (horizontal, vertical, diagonal)
        int[][] directions = new int[][]
        {
        new int[] { 1, 0 },   // bas
        new int[] { -1, 0 },  // haut
        new int[] { 0, 1 },   // droite
        new int[] { 0, -1 },  // gauche
        new int[] { 1, 1 },   // bas droite
        new int[] { 1, -1 },  // bas gauche
        new int[] { -1, 1 },  // haut droite
        new int[] { -1, -1 }  // haut gauche
        };

        // Parcours chaque direction
        foreach (int[] dir in directions)
        {
            int l = ligne + dir[0];
            int c = colonne + dir[1];

            // limite du plateau
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
                    break; // on ne peut pas sauter par-dessus une pièce
                }
                else
                {
                    // Pièce alliée 
                    break;
                }

                // Avancer dans la même direction
                l += dir[0];
                c += dir[1];
            }
        }

        return coups;
    }
    public override Piece Cloner()
    {
        Reine clone = new Reine((int[,])getTableau().Clone(), getLigne(), getColonne(), getCouleur());
        clone.setValeur(this.getValeur());
        if (!this.isFixe()) clone.setFixe();
        clone.setSymbole(this.getSymbole());
        if (this.isEchec() != null) clone.setEchec((bool[,])this.isEchec().Clone());
        return clone;
    }
    /*
    public override Piece Cloner()
    {
        return new Reine((int[,])getTableau().Clone(), getLigne(), getColonne(), getCouleur());
    }
    */
}
