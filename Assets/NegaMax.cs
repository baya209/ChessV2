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
    public int NegaMax(Plateau plateau, int profondeur, int alpha, int beta, int couleur)
    {

        //if (profondeur == 0 || plateau.EstFinDePartie())
        {
            return EvaluerEchiquier(plateau, couleur);
        }
        int meilleurScore = -valeurMax;
        //logique du plateau?
        //List<Coup> coupsPossibles = GenererTousLesCoups(plateau, couleur);
       // foreach (Coup coup in coupsPossibles)
        {
            // Jouer le coup
            //JouerCoup(plateau, coup);

            // Appel recursif en inversant le joueur
            int score = -NegaMax(plateau, profondeur - 1, -beta, -alpha, InverserCouleur(couleur));

            // Annuler le coup pour revenir à l'état précédent
            //AnnulerCoup(plateau, coup);

            meilleurScore = Mathf.Max(meilleurScore, score);

            //TODO: Appliquer l'elagage alpha-bêta

        }
        return meilleurScore;

    }



    public Coup TrouverMeilleurCoup(Plateau plateau, int couleur)
    {
        int meilleurScore = -valeurMax;
        Coup meilleurCoup = null;

        List<Coup> coupsPossibles = GenererTousLesCoups(plateau, couleur);


        foreach (Coup coup in coupsPossibles)
        {
            JouerCoup(plateau, coup);
            int score = -NegaMax(plateau, profondeurMax - 1, -valeurMax, valeurMax, -couleur);
            AnnulerCoup(plateau, coup);

            if (score > meilleurScore)
            {
                meilleurScore = score;
                meilleurCoup = coup;
            }
        }

  
        return meilleurCoup;

    }


    public void JouerCoup(Plateau plateau, Coup coup)
    {
        plateau.getTableau()[coup.lf, coup.cf] = plateau.getTableau()[coup.li, coup.ci];
        plateau.getTableau()[coup.li, coup.ci] = 5; // Case vide après le déplacement
    }

    //methode pour revenir en arriere
    public void AnnulerCoup(Plateau plateau, Coup coup)
    {
        plateau.getTableau()[coup.li, coup.ci] = plateau.getTableau()[coup.lf, coup.cf];
        plateau.getTableau()[coup.lf, coup.cf] = coup.pieceCapturee;
    }

    public List <Coup> GenererTousLesCoups(Plateau plateau, int couleur)
    {
        List<Coup> coupsPossibles = new List<Coup>();

        foreach (Piece piece in plateau.getPieces())
        {

            if (piece.getCouleur() == couleur)
            {
                /*List<(int, int)> mouvements = piece.GenererMouvementsPossibles(plateau);
                foreach ((int lf, int cf) in mouvements)
                {
                    int pieceCapturee = plateau.getTableau()[lf, cf];
                    coupsPossibles.Add(new Coup(piece.getLigne(), piece.getColonne(), lf, cf, pieceCapturee));
                }*/
            }

        }



        return coupsPossibles;
    }

 
    //TODO: generer tout les coups

    
    public int InverserCouleur(int couleur)
    {
        couleur = -(couleur);
        
        return couleur;

    }
    public int EvaluerEchiquier(Plateau plateau, int couleur)
    {
        int score = 0;
        //score materiel

        foreach(Piece piece in plateau.getPieces())//parcourir le tableau de piece
        {
            int valeur = piece.getValeur();
            // si couleur cest la meme que le joueur actuel alors on ajoute au score 
            int couleurpiecearray = piece.getCouleur();

            score += (couleurpiecearray == couleur ) ? valeur : -valeur; 

        }
        score += EvaluerControleCentre( plateau, couleur);


        return score;
    }


    


    
       //tableau
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
    
    public int EvaluerControleCentre(Plateau plateau, int joueurActuel)
    {
        int score = 0;
        foreach (Piece piece in plateau.getPieces())

        {
            int bonus = bonusCentre[piece.getLigne(), piece.getColonne()];
            int valeurPiece = piece.getValeur();


            //si la piece est un roi on enleve le bonus 
            if (piece is Roi)
            {

                //TODO: ajout de la variante dependemment de la phase de la partie
                //fin de phase le bonus reste positif
                bonus = -bonus;
            }

            // Appliquer le bonus en fonction de la piece
            //ne pas etre applique aux pions
            score += (piece.getCouleur() == joueurActuel) ? bonus * (valeurPiece > 100 ? 1 : 0) : -bonus * (valeurPiece > 100 ? 1 : 0);

        }

        return score;
    
    }



    
    

}

