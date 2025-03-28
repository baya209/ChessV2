using System;
public class Reine : Piece
{
    public Reine(int[,] tableau, int ligne, int colonne, int couleur) : base(tableau, ligne, colonne, couleur)
    {
        setSymbole('re');
    }

    public override bool deplacer(int l, int c)
    {
        return true
    }

    public override bool[,] isDanger(bool[,] danger)
    {
        return true
    }

}
