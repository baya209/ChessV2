
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Plateau 
{
    
    private bool[,] dangerNoir = new bool[8,8];
    private bool[,] dangerBlanc = new bool[8, 8];
    private int[,] tableauModulo = new int[8,8];
    /*
    2 = blanc
    3 = noir
    5 = vide
    7 = zone danger noir
    11 = zone danger blanc
    13 = pion
    17 = tour
    19 = cavalier
    23 = fou
    29 = reine
    31 = roi
     */


    private int[,] tableau = new int[8, 8]; // -1 = noir /// 0 = vide /// 1= blanc
    List<Piece> pieces = new List<Piece>();
    
    public Plateau(int[,] tableau, List<Piece> pieces)
    {
        this.pieces = pieces;
        this.tableau = tableau; 
    }
    public int[,] getTableau() {  return tableau; }
    public void setTableau(in int[,] tableau)
    {
        this.tableau = tableau;
    }
    public int[,] getTableauModulo() { return tableauModulo; }
    public void setTableauModulo( int[,] tableauModulo)
    {
        this.tableauModulo = tableauModulo;
    }
    public void setPieces(List<Piece> pieces) {  this.pieces = pieces; }
    public List<Piece> getPieces() { return pieces; }
    public void setDangerNoir(bool[,] dangerNoir)
    {
        this.dangerNoir = dangerNoir;
    }
    public bool[,] getDangerNoir() { return dangerNoir; }
    public void setDangerBlanc(bool[,] dangerBlanc)
    {
        this.dangerBlanc = dangerBlanc;
    }
    public bool[,] getDangerBlanc() { return dangerBlanc; }


    // ecrire deplacement speciaux dans jeu
    public List<Coup> GenererTousLesCoups(int couleur)
    {
        List<Coup> tousLesCoups = new List<Coup>();

        foreach (Piece piece in pieces)
        {
            if (piece.getCouleur() == couleur)
            {
                List<Coup> coups = piece.GenererCoupsPossibles(this);
                tousLesCoups.AddRange(coups);
            }
        }

        return tousLesCoups;
    }


    public void JouerCoupIA(Coup coup)
    {
        // 1. Trouver la pièce à déplacer
        Piece pieceAJouer = pieces.Find(p => p.getLigne() == coup.li && p.getColonne() == coup.ci);
        if (pieceAJouer == null)
        {
            Debug.LogError("Erreur : pièce non trouvée pour JouerCoupIA");
            return;
        }

        // 2. Supprimer la pièce adverse si présente
        Piece pieceCapturee = pieces.Find(p => p.getLigne() == coup.lf && p.getColonne() == coup.cf);
        if (pieceCapturee != null)
        {
            pieces.Remove(pieceCapturee);
        }

        // 3. Mettre à jour le tableau
        tableau[coup.lf, coup.cf] = tableau[coup.li, coup.ci];
        tableau[coup.li, coup.ci] = 0;

        // 4. Mettre à jour la pièce
        pieceAJouer.setLigne(coup.lf);
        pieceAJouer.setColonne(coup.cf);
        pieceAJouer.setFixe(); // utile pour roque
    }
    public Plateau CopierPlateau()
    {
        // 1. Copier le tableau (copie profonde)
        int[,] tableauCopie = new int[8, 8];
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                tableauCopie[i, j] = this.tableau[i, j];

        // 2. Copier les pièces
        List<Piece> piecesCopiees = new List<Piece>();
        foreach (Piece p in this.pieces)
        {
            Piece clone = p.Cloner();              // Cloner la pièce
            clone.setTableau(tableauCopie);        // Très important : tableau mis à jour
            piecesCopiees.Add(clone);
        }

        // 3. Créer le nouveau plateau
        Plateau copie = new Plateau(tableauCopie, piecesCopiees);

       

        return copie;
    }


}
