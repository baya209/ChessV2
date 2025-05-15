using System.Collections.Generic;
using UnityEngine;



public class Partie 
{
    public Partie()
    {
        
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
        Roi roiNoir = new Roi(tableau,4,7,-1);
        Roi roiBlanc = new Roi(tableau, 4, 0, 1);
        Tour tourN1 = new Tour(tableau, 0, 7, -1);
        Tour tourN2 = new Tour(tableau, 7, 7, -1);
        Tour tourB1 = new Tour(tableau, 0, 0, 1);
        Tour tourB2 = new Tour(tableau, 7, 0, 1);
        Reine reineBlanc = new Reine(tableau, 3, 0, 1);
        Reine reineNoir = new Reine(tableau, 3, 7, -1);
        Fou fouB1 = new Fou(tableau, 2, 0, 1);
        Fou fouB2 = new Fou(tableau, 5, 0, 1);
        Fou fouN1 = new Fou(tableau, 2, 7, -1);
        Fou fouN2 = new Fou(tableau, 5, 7, -1);
        Cavalier cavalierB1 = new Cavalier(tableau, 1, 0, 1);
        Cavalier cavalierB2 = new Cavalier(tableau, 6, 0, 1);
        Cavalier cavalierN1 = new Cavalier(tableau, 1, 7, -1);
        Cavalier cavalierN2 = new Cavalier(tableau, 6, 7, -1);
        pieces.Add(reineBlanc);
        pieces.Add(reineNoir);
        pieces.Add(tourN2);
        pieces.Add(tourN1);
        pieces.Add(tourB2);
        pieces.Add(tourB1);
        pieces.Add(roiBlanc);
        pieces.Add(roiNoir);
        pieces.Add(fouB1);
        pieces.Add(fouB2);
        pieces.Add(fouN1);
        pieces.Add(fouN2);
        pieces.Add(cavalierB1);
        pieces.Add(cavalierB2);
        pieces.Add(cavalierN1);
        pieces.Add(cavalierN2);
        plateau = new Plateau(tableau,pieces);
    }
    
    public bool jouerCoup(int li, int ci, int lf,int cf, int couleur)
    {
        if (li < 0 || lf < 0 || cf < 0 || ci < 0 || li > 7 || lf > 7 || ci > 7 || cf > 7) { 
        
        return false;
        }
        
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

                return true;
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
    private bool castling(int li, int ci, int lf, int cf, Piece piece)
    {
        Piece tour;
        if (piece.isFixe())
        {
            if (li == 4 && ci == 0)
            {
                if (plateau.getTableau()[5, 0] == 0 && plateau.getTableau()[6, 0] == 0 && plateau.getTableau()[7,0]==1)
                {
                    tour = plateau.getPieces().Find(p => p.getLigne() == 7 && p.getColonne() == 0);
                    if (tour is Tour && tour.isFixe())
                    {
                        plateau.getTableau()[4,0] = 0;
                        plateau.getTableau()[7, 0] = 0;
                        plateau.getTableau()[5, 0] = 1;
                        plateau.getTableau()[6, 0] = 1;
                        piece.setLigne(6);
                        tour.setLigne(5);
                        return true;
                    }
                }
                else if (plateau.getTableau()[0, 0] == 1 && plateau.getTableau()[1, 0] == 0 && plateau.getTableau()[2, 0] == 0 && plateau.getTableau()[3, 0] == 0)
                {
                    tour = plateau.getPieces().Find(p => p.getLigne() == 0 && p.getColonne() == 0);
                    if (tour is Tour && tour.isFixe())
                    {
                        
                        plateau.getTableau()[0, 0] = 0;
                        plateau.getTableau()[4, 0] = 0;
                        plateau.getTableau()[1, 0] = 1;
                        plateau.getTableau()[2, 0] = 1;
                        piece.setLigne(1);
                        tour.setLigne(2);
                        return true;
                    }
                }
            }
            if (li == 4 && ci == 7)
            {
                if (plateau.getTableau()[5, 7] == 0 && plateau.getTableau()[6, 7] == 0 && plateau.getTableau()[7, 7] == -1)
                {
                    tour = plateau.getPieces().Find(p => p.getLigne() == 7 && p.getColonne() == 7);
                    if (tour is Tour && tour.isFixe())
                    {
                        plateau.getTableau()[4, 7] = 0;
                        plateau.getTableau()[7, 7] = 0;
                        plateau.getTableau()[5, 7] = -1;
                        plateau.getTableau()[6, 7] = -1;
                        piece.setLigne(6);
                        tour.setLigne(5);
                        return true;
                    }
                }
                else if (plateau.getTableau()[0, 7] == -1 && plateau.getTableau()[1, 7] == 0 && plateau.getTableau()[2, 7] == 0 && plateau.getTableau()[3, 7] == 0)
                {
                    tour = plateau.getPieces().Find(p => p.getLigne() == 0 && p.getColonne() == 7);
                    if (tour is Tour && tour.isFixe())
                    {
                        plateau.getTableau()[0, 7] = 0;
                        plateau.getTableau()[4, 7] = 0;
                        plateau.getTableau()[2, 7] = -1;
                        plateau.getTableau()[1, 7] = -1;
                        piece.setLigne(1);
                        tour.setLigne(2);
                        return true;
                    }
                }
            }
        }
        
        return false;

    }
    private bool special(int li, int ci, int lf, int cf, Piece piece)
    {
        if(piece is Pion)
        {
            if(enPassant( li,  ci,  lf,  cf, piece))
            {
                return true;
            }
            if (piece.deplacer(lf,cf) && (cf == 7 || cf == 0)) {
                
                plateau.getPieces().Add(new Tour(plateau.getTableau(), lf, cf, piece.getCouleur()));//
                plateau.getPieces().Remove(piece);
                return true;
            }
            
        } 
        else if(piece is Roi)
        {
            if (castling(li, ci, lf, cf, piece)) { 
               return true;
            }
        }
        return false;
    }
    public bool enPassant(int li, int ci, int lf, int cf, Piece piece)
    {
        if (plateau.getTableau()[lf,cf] != 0) {  return false; }    
        if (plateau.getTableau()[li, cf] == -1*piece.getCouleur()) {
            Pion pion = plateau.getPieces().Find(p => p.getLigne() == li && p.getColonne() == cf) as Pion; 
            if(piece.getCouleur() == 1)
            {
                if (li == 4 && lf == 5 && ((cf == ci + 1 ) || (cf == ci - 1 )))
                {
                    if (pion.getNbreDeplacement() == 1)
                    {
                        plateau.getTableau()[lf, cf] = 1;
                        plateau.getTableau()[li, cf] = 0;
                        piece.setLigne(lf);
                        piece.setColonne(cf);
                        plateau.getPieces().Remove(pion);
                        return true;
                    }
                    
                }
            }
            else if (piece.getCouleur() == -1)
            {
                if (li == 3 && lf == 2 && ((cf == ci + 1) || (cf == ci - 1)))
                {
                    if (pion.getNbreDeplacement() == 1)
                    {
                        plateau.getTableau()[lf, cf] = -1;
                        plateau.getTableau()[li, cf] = 0;
                        piece.setLigne(lf);
                        piece.setColonne(cf);
                        plateau.getPieces().Remove(pion);
                        return true;
                    }

                }
            }
            
        }

        return false;
    }

    public string afficher()
    {
        string[,] affichage = new string[8,8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (plateau.getTableau()[i,j] == -1)
                {
                    affichage[i, j] = "N";
                    
                }
                else if(plateau.getTableau()[i, j] == 0)
                {
                    affichage[i, j] = "00";
                }
                else
                {
                    affichage[i, j] = "B";
                }
                
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
        return final;
        
    }
    
    public void choisir()
    {

    }
    public void setPlateau(Plateau plateau)
    {
        this.plateau = plateau;
    }
    public Plateau getPlateau() { return this.plateau; }
   

}