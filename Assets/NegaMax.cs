using System;
using System.Collections.Generic;
using UnityEngine;

public class NegaMaxScript
{

    /* 
    * place vide  = 0
    * pions = 100
    * cavalier = 300
    * fou = 350
    * tour = 500
    * dame = 1000
    * roi = 10 000
    * pour piece noir -> mettre valeur en negatif
    * */

    //pour le controle du centre 
    int[,] bonusCentre = {
    { 0, 0, 1, 2, 2, 1, 0, 0 },
    { 0, 1, 2, 3, 3, 2, 1, 0 },
    { 1, 2, 3, 4, 4, 3, 2, 1 },
    { 2, 3, 4, 5, 5, 4, 3, 2 },
    { 2, 3, 4, 5, 5, 4, 3, 2 },
    { 1, 2, 3, 4, 4, 3, 2, 1 },
    { 0, 1, 2, 3, 3, 2, 1, 0 },
    { 0, 0, 1, 2, 2, 1, 0, 0 }
    };

    public static int profondeurMax = 3;

    //regarde recursivement chaque coup
    public int NegaMax(Plateau plateau, int profondeur, int alpha, int beta, int couleur)
    {
        if (profondeur == 0) //si a la fin on retourne un score
            return EvaluerEchiquier(plateau, couleur);

        int meilleurScore = int.MinValue;
        List<Coup> coups = plateau.GenererTousLesCoups(couleur); //genere tout selon la couleur

        if (coups.Count == 0)// aucune option et
            return EvaluerEchiquier(plateau, couleur);
        foreach (Coup coup in coups)
        {
            Plateau copie = plateau.CopierPlateau();
            copie.JouerCoupIA(coup);//joue sur la copie
            int score = -NegaMax(copie, profondeur - 1, -beta, -alpha, -couleur);

            if (score > meilleurScore)
                meilleurScore = score;

            alpha = Math.Max(alpha, score);
            if (alpha >= beta)
                break; // coupure
        }

        return meilleurScore;
    }

    //choisi coups final a joueur a utilise pour UNITY
    public Coup SuggererCoup(Plateau plateau, int couleur)
    {
        List<Coup> coups = plateau.GenererTousLesCoups(couleur);
        int meilleurScore = int.MinValue;
        Coup meilleurCoup = null;

        foreach (Coup coup in coups)
        {
            Plateau copie = plateau.CopierPlateau();
            copie.JouerCoupIA(coup);
            int score = -NegaMax(copie, profondeurMax - 1, int.MinValue, int.MaxValue, -couleur);

            if (score > meilleurScore || meilleurCoup == null)
            {
                meilleurScore = score;
                meilleurCoup = coup;
            }
        }

        return meilleurCoup;
    }
    
    


    public int EvaluerEchiquier(Plateau plateau, int couleur)
    {
        int score = 0;
        int[,] tableau = plateau.getTableau();

        foreach (Piece piece in plateau.getPieces())
        {
            int val = piece.getValeur();
            int ligne = piece.getLigne();
            int colonne = piece.getColonne();
            int pieceCouleur = piece.getCouleur();

            // Score mat?riel (selon allie ou ennemi)
            score += (pieceCouleur == couleur) ? val : -val;

            // Bonus positionnel selon le tableau bonusCentre (exclut les pions)
            if (!(piece is Pion))
            {
                int bonus = bonusCentre[ligne, colonne];

                // Si c'est un roi, on enl?ve le bonus sauf en finale a travailler 
                if (piece is Roi)
                    bonus = -bonus;

                score += (pieceCouleur == couleur) ? bonus : -bonus;
            }

            // bonus si roi est entouree
            if (piece is Roi)
            {
                int allieeAutour = 0;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue;

                        int x = ligne + dx;
                        int y = colonne + dy;

                        if (x >= 0 && x < 8 && y >= 0 && y < 8)
                        {
                            int pieceAdj = tableau[x, y];
                            if (pieceAdj != 5 && pieceAdj % 2 == pieceCouleur % 2) // m?me couleur
                                allieeAutour++;
                        }
                    }
                }

                score += (pieceCouleur == couleur) ? allieeAutour * 3 : -allieeAutour * 3;
            }

            // Pion avanc? sur le plateau  et dangereux = +score
            if (piece is Pion)
            {
                bool estPionAvance = (pieceCouleur == 1 && ligne <= 3) || (pieceCouleur == -1 && ligne >= 4);
                if (estPionAvance)
                    score += (pieceCouleur == couleur) ? 30 : -30;
            }


        }

        return score;
    }





}

