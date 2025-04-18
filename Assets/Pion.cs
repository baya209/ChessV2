
public class Pion : Piece
{
    public Pion(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('P');
    }

    public override bool deplacer(int l, int c){//  c colonne finale, l ligne finale

        if(getTableau()[l, c] == 0)//Verifie que la case est vide 
        {
            if (c == (getColonne() + getCouleur()) && l == getLigne())// Pion qui avance d'une case
            {
                return true;
            }
            else if(getTableau()[l, c - 1] == 0&& (c == (getColonne() + 2) && (getCouleur() == 1 && getColonne() == 1)))// Pion blanc qui avance de deux cases  
            {
                return true;
            }
            else if (getTableau()[l, c+1] == 0 && getCouleur() == -1 && getColonne() == 7 && c == (getColonne() - 2)) // Pion noir qui avance de deux cases
            {
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
}
