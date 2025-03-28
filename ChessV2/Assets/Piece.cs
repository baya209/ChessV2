
public abstract class Piece 
{
    private int colonne; // exemple la reine blanche est a la position au depart (3,0)
    private int ligne;
    private bool[,] echec;
    private int valeur; //Valeur de NegaMax
    private int couleur; // -1 noir 1 blanc
    private int[,] tableau = new int[8, 8]; // -1 = noir /// 0 = vide /// 1= blanc
    private bool fixe = true; // Piece a elle bouge: Pour Castling
    private char symbole;
    public Piece(int[,] tableau,int ligne, int colonne, int couleur)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        this.couleur = couleur;
        this.tableau = tableau;
    }

    protected Piece(int[,] tableau)
    {
        this.tableau = tableau;
    }

    public abstract bool deplacer(int l, int c);
    public int getColonne() {
        return colonne;
    }
    public int getLigne()
    {
        return ligne;
    }
    public int getCouleur()
    {
        return couleur;
    }
    public void setColonne( int colonne) { this.colonne = colonne; }
    public void setLigne(int ligne) { this.ligne = ligne; }
    public int getValeur() {
        return valeur;  
    }
    public void setValeur(int valeur)
    {
        this.valeur = valeur;    
    }
    public void setTableau(int[,] tableau)
    {
        this.tableau = tableau;
    }
    public int[,] getTableau() { 
    return tableau;
    }
    public abstract bool[,] isDanger(bool[,]danger);
    public void setFixe() {
        fixe = false;
    }
    public bool isFixe() { return fixe; }
    public void setEchec(bool[,] echec)
    {
        this.echec = echec;
    }
    public bool[,] isEchec()
    {
        return echec;
    }
    public char getSymbole()
    {
        return symbole;
    }
    public void setSymbole(char symbole)
    {
        this.symbole = symbole;
    }

}
