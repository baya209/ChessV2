using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript
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


    public int profondeurMax = 3; //sera changer par une constante
    public int valeurMax = 1000000;
    public int NegaMax(Plateau plateau, int profondeur, int alpha, int beta, Couleur joueurActuel)
    {

        if (profondeur == 0 || plateau.EstFinDePartie())
        {
            return EvaluerEchiquier(plateau, joueurActuel);
        }


        int meilleurScore = -valeurMax;
        //logique du plateau?
        List<Coup> coupsPossibles = GenererTousLesCoups(plateau, joueurActuel);
        foreach (Coup coup in coupsPossibles)
        {
            // Jouer le coup
            JouerCoup(plateau, coup);

            // Appel recursif en inversant le joueur
            int score = -NegaMax(plateau, profondeur - 1, -beta, -alpha, InverserCouleur(joueurActuel));

            // Annuler le coup pour revenir à l'état précédent
            AnnulerCoup(plateau, coup);

            meilleurScore = Mathf.Max(meilleurScore, score);

            //TODO: Appliquer l'elagage alpha-bêta

        }
        return meilleurScore;

    }
    public Coup TrouverMeilleurCoup(Plateau plateau, Couleur joueurActuel)
    {
        int meilleurScore = -valeurMax;
        Coup meilleurCoup = null;
        List<Coup> coupsPossibles = GenererTousLesCoups(plateau, joueurActuel);

        foreach (Coup coup in coupsPossibles)
        {
            JouerCoup(plateau, coup);
            int score = -NegaMax(plateau, profondeurMax - 1, -valeurMax, valeurMax, InverserCouleur(joueurActuel));
            AnnulerCoup(plateau, coup);

            //comparer les score pour etablir le meilleur
            if (score > meilleurScore)
            {
                meilleurScore = score;
                meilleurCoup = coup;
            }
        }
        return meilleurCoup;
    }
    //TODO: methode inversion de couleur joueur
    //TODO: generer tout les coups



    public int EvaluerEchiquier(Plateau plateau, Couleur joueurActuel)
    {
        int score = 0;
        //score materiel
        foreach(Piece piece in plateau.pieces)//parcourir le tableau de piece
        {
            int valeur = GetValeurPieces(piece); 
            // si couleur cest la meme que le joueur actuel alors on ajoute au score 

            score += (piece.getCouleur == joueurActuel) ? valeur : -valeur; 

        }
        score += EvaluerControleCentre(Plateau plateau,joueurActuel);


        return score;
    }


    


    /*
     *   //tableau
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
    */
    public int EvaluerControleCentre(Plateau plateau, Couleur joueurActuel)
    {
        int score = 0;
        foreach (Piece piece in plateau.pieces)
        {
            int bonus = bonusCentre[piece.Position.x, piece.Position.y];
            int valeurPiece = GetValeurPieces(piece);


            //si la piece est un roi on enleve le bonus 
            if (piece is Roi)
            {

                //TODO: ajout de la variante dependemment de la phase de la partie
                //fin de phase le bonus reste positif
                bonus = -bonus;
            }

            // Appliquer le bonus en fonction de la piece
            //ne pas etre applique aux pions
            score += (piece.getCouleur == joueurActuel) ? bonus * (valeurPiece > 100 ? 1 : 0) : -bonus * (valeurPiece > 100 ? 1 : 0);

        }

        return score;

    }






}



