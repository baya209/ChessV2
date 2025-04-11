
using System.Collections.Generic;


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

}
