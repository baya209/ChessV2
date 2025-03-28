using System.Collections.Generic;
using UnityEngine;



public class Partie 
{
    public Partie()
    {
        creerPartie();
    }
    private Plateau plateau;
    public void creerPartie()
    {
        int[,] tableau = new int[8, 8];
        //Creation du plateau
        for (int i = 0; i < 8; i++)
        {
            tableau[i,0] = 1;
            tableau[i,1] = 1;

            tableau[i, 2] = 0;
            tableau[i, 3] = 0;
            tableau[i, 4] = 0;
            tableau[i, 5] = 0;

            tableau[i,6] = -1;
            tableau[i, 7] = -1;
        }
        List<Piece> pieces = new List<Piece>();
        //Ajout pions 
        for (int i = 0; i < 8; i++) {
            
            Pion pion1 = new Pion(tableau, i, 1, 1);
            Pion pion3 = new Pion(tableau, i, 6, -1);
            pieces.Add(pion1);
            pieces.Add(pion3);
            
        }
        Roi roiNoir = new Roi(tableau,7,4,-1);
        Roi roiBlanc = new Roi(tableau, 4, 0, 1);
        pieces.Add(roiBlanc);
        pieces.Add(roiNoir);
        plateau = new Plateau(tableau,pieces);
    }
    
    public bool jouerCoup(int li, int ci, int lf,int cf, int couleur)
    {
        
        evaluerDanger();
        foreach (var piece in plateau.getPieces())
        {
            if (piece is Roi)
            {
                if (piece.getCouleur() == 1)
                {
                    piece.setEchec(plateau.getDangerBlanc());
                }
                else
                {
                    piece.setEchec(plateau.getDangerNoir());
                }
            }
        }
        if (plateau.getTableau()[li,ci] == couleur)
        {
            Piece deplace = plateau.getPieces().Find(p => p.getLigne() == li && p.getColonne() == ci);
            if ((special(li, ci, lf, cf, deplace)))
            {
                return true;
            }
            if (deplace.deplacer(lf, cf))
            {
                deplace.setLigne(lf);
                deplace.setColonne(cf);
                deplace.setFixe();
                if (plateau.getPieces().Find(p => p.getLigne() == lf && p.getColonne() == cf).getCouleur() == deplace.getCouleur() * -1)
                {
                    plateau.getPieces().Remove(plateau.getPieces().Find(p => p.getLigne() == lf && p.getColonne() == cf));
                }
                plateau.getTableau()[li, ci] = 0;
                plateau.getTableau()[lf, cf] = deplace.getCouleur();


            }
            
            return false;
        }
        else
        {

        }
        return false;
    }
    public void evaluerDanger()
    {
        bool[,] dangerNoir = new bool[8, 8];
        bool[,] dangerBlanc = new bool[8, 8];
        for (int i = 0;  i < plateau.getPieces().Count; i++) {
            if (plateau.getPieces()[i].getCouleur() == -1)
            {
                dangerBlanc = plateau.getPieces()[i].isDanger(dangerBlanc);
            }
            else
            {
                dangerNoir = plateau.getPieces()[i].isDanger(dangerNoir);
            }
        }
        plateau.setDangerNoir(dangerNoir);
        plateau.setDangerBlanc(dangerBlanc);
    }
    private bool castling(int li, int ci, int lf, int cf)
    {
        return false;
    }
    private bool special(int li, int ci, int lf, int cf, Piece piece)
    {
        if(piece.GetType() == typeof(Pion))
        {
            if (piece.deplacer(lf,cf) && (cf == 7 || cf == 0)) {
                plateau.getPieces().Add(new Tour(plateau.getTableau(), lf, cf, piece.getCouleur()));//
                plateau.getPieces().Remove(piece);
                return true;
            }
        } 
        return false;
    }

    public void afficher()
    {
        string[,] affichage = new string[8,8];
        for (int i = 0; i < plateau.getTableau().GetLength(0); i++)
        {
            for (int j = 0; j < plateau.getTableau().GetLength(1); j++)
            {
                affichage[i, j] = plateau.getTableau()[i, j].ToString();
            }
        }
        foreach (var piece in plateau.getPieces()) {
            affichage[piece.getLigne(), piece.getColonne()] += piece.getSymbole();
        }
        string final = "";
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                final += affichage[j,i]+ "/";
            }
            final += "\n";
        }
        Debug.Log(final);
        
    }
}