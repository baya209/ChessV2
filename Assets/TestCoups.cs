using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoups : MonoBehaviour
{
    void Start()
    {
        // Crée un échiquier vide
        int[,] tableau = new int[8, 8];

        // Place un pion blanc en (6,4) (ligne de départ)
        tableau[6, 4] = 1;

        // Place des ennemis (noirs = -1) sur les diagonales avant
        tableau[5, 3] = -1; // diagonale gauche → capturable
        tableau[5, 5] = -1; // diagonale droite → capturable

        // Crée le pion blanc
        Pion pion = new Pion(tableau, 6, 4, 1);

        // Génère les coups
        List<Coup> coups = pion.GenererCoupsPossibles();

        // Affiche tous les coups possibles
        foreach (Coup coup in coups)
        {
            Debug.Log($"Pion : ({coup.li},{coup.ci}) → ({coup.lf},{coup.cf}) Capture: {coup.pieceCapturee}");
        }
    }
}
