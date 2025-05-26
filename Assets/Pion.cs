
using System.Collections.Generic;

public class Pion : Piece
{
    // Essentiel pour pouvoir gérer les déplacements particuliés du pion
    private int nbreDeplacement = 0;
    public Pion(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('P');
        setValeur(100);
    }
    public int getNbreDeplacement()
    {
        return nbreDeplacement;

    }
    public override bool deplacer(int l, int c){//  c colonne finale, l ligne finale

        if(getTableau()[l, c] == 0)//Verifie que la case est vide 
        {
            if (c == (getColonne() + getCouleur()) && l == getLigne())// Pion qui avance d'une case
            {
                nbreDeplacement ++;
                return true;
            }
            else if(getTableau()[l, c - 1] == 0&& (c == (getColonne() + 2) && (getCouleur() == 1 && getColonne() == 1)))// Pion blanc qui avance de deux cases  
            {
                nbreDeplacement++;
                return true;
            }
            else if (getTableau()[l, c+1] == 0 && getCouleur() == -1 && getColonne() == 6 && c == (getColonne() - 2)) // Pion noir qui avance de deux cases
            {
                nbreDeplacement++;
                return true;
            }
        }
        else if (getTableau()[l, c] == (-1*getCouleur()))// verifie si la case est occupee par une piece adverse
        {
            if(l == (getLigne() + 1) || l == (getLigne() - 1) ) // verifie si le movement est decale sur la ligne
            {
                if (c == (getColonne() + getCouleur()) )// Pion qui avance d'une case                       
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override bool[,] isDanger(bool[,] danger)
    {
        if (getCouleur() == 1)
        {
            if(getColonne() < 7)
            {
                if(getLigne() != 7)
                {
                    danger[getLigne() + 1, getColonne() + 1] = true;
                }
                if (getLigne() != 0) {
                    danger[getLigne() - 1, getColonne() + 1] = true;
                } 
            }
            
        }
        else if (getCouleur() == -1)
        {
            if (getColonne() > 0)
            {
                if (getLigne() != 7)
                {
                    danger[getLigne() + 1, getColonne() - 1] = true;
                }
                if (getLigne() != 0)
                {
                    danger[getLigne() - 1, getColonne() + 1] = true;
                }
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

        int direction = -couleur; // blanc avance vers le haut (direction = -1), noir vers le bas (+1)
        int ligneAvant = ligne + direction;

        // Mouvement de 1 case vers l'avant
        if (ligneAvant >= 0 && ligneAvant < 8 && t[ligneAvant, colonne] == 0)
        {
            coups.Add(new Coup(ligne, colonne, ligneAvant, colonne, -1));

            // Mouvement de 2 cases depuis la ligne de départ si la 2e case est libre aussi
            int ligneDepart = (couleur == 1) ? 6 : 1;
            int ligneDeux = ligne + 2 * direction;

            if (ligne == ligneDepart && ligneDeux >= 0 && ligneDeux < 8 && t[ligneDeux, colonne] == 0)
            {
                coups.Add(new Coup(ligne, colonne, ligneDeux, colonne, -1));
            }
        }

        // Captures diagonales (avant gauche et avant droite)
        for (int dc = -1; dc <= 1; dc += 2)
        {
            int c = colonne + dc;
            if (c >= 0 && c < 8 && ligneAvant >= 0 && ligneAvant < 8)
            {
                int cible = t[ligneAvant, c];
                if (cible != 0 && cible != couleur)
                {
                    coups.Add(new Coup(ligne, colonne, ligneAvant, c, cible));
                }
            }
        }

        return coups;
    }
    public override Piece Cloner()
    {
        return new Pion((int[,])getTableau().Clone(), getLigne(), getColonne(), getCouleur()); 
    }
    
}
