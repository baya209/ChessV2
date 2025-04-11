using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Coup
{
    public int li, ci, lf, cf;
    public int pieceCapturee;// valeur piece capturee (-1 si il n'y a pas de piece)

    public Coup(int li, int ci, int lf, int cf, int pieceCapturee = -1)
    {
        this.li = li;
        this.ci = ci;
        this.lf = lf;
        this.cf = cf;
        this.pieceCapturee = pieceCapturee;
    }
}
