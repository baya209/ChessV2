using System;
public class Fou : Piece
{
    public Fou(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('F');
    }

    public override bool deplacer(int l, int c)
    {
        return true;
    }

    public override bool[,] isDanger(bool[,] danger)
    {
        return danger;
    }

}
