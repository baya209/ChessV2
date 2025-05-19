using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text echiquier;
    Partie partie = new Partie();

    [SerializeField] private PlateuDeJeu plateuDeJeu;
    public GameObject[] piecePrefabs;

    public InputField fli;
    public string li;
    public string ci;
    public string lf;
    public string cf;
    public int ili;
    public InputField inputField;
    string user;

    GameObject[,] pieces3D = new GameObject[8, 8];

    private int nbreDePieceMangeB = 0;
    private int nbreDePieceMangeN = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        partie.creerPartie();
        echiquier.text = partie.afficher();
        List<Piece> pieces = partie.getPlateau().getPieces();
        

        placerPiece3D(pieces,pieces3D);
        
    }
  
    // Update is called once per frame
    void Update()
    {
        user = inputField.text;
        


        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("enter");
            // Split and trim
            string[] stg = user.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
                               .Select(s => s.Trim())
                               .ToArray();

            // Check if we got exactly 5 numbers
            if (stg.Length != 5)
            {
                Debug.LogWarning("Invalid input. Please enter 5 comma-separated numbers.");
                return;
            }

            // Try parsing all
            int[] diviser = new int[5];
            for (int i = 0; i < 5; i++)
            {
                if (!int.TryParse(stg[i], out diviser[i]))
                {
                    Debug.LogWarning($"Invalid number at position {i + 1}: '{stg[i]}'");
                    return;
                }
            }

            // Call your game logic
            bool jouer = partie.jouerCoup(diviser[0], diviser[1], diviser[2], diviser[3], diviser[4]);

            if (jouer == true)
            {
                deplacerPiece3D(diviser[1], diviser[0], diviser[3], diviser[2], diviser[4]);
                Debug.Log("PIECE DEPLACEMENT AVEC SUCCES");
            }
            
            

            echiquier.text = partie.afficher();
        
        }
        
        
    }

    public void placerPiece3D(List<Piece> pieces, GameObject[,] pieces3D)
    {
foreach (var piece in pieces)
        {
            GameObject piece3D = new GameObject();
            
            if (piece is Pion)
            {
               piece3D.layer = LayerMask.NameToLayer("Pion");
               
                Vector3 pos = plateuDeJeu.GetCentreTuile(piece.getColonne(), piece.getLigne());

                if(piece.getCouleur() == 1)
                    piece3D = Instantiate(piecePrefabs[0], pos, piecePrefabs[0].transform.rotation );
                else
                    piece3D = Instantiate(piecePrefabs[1], pos, piecePrefabs[1].transform.rotation);

                pieces3D[piece.getColonne(), piece.getLigne()] = piece3D;
                Debug.Log("Nouvelle Pièce");
            }
            
               else if (piece is Roi)
               {
                    piece3D.layer = LayerMask.NameToLayer("Roi");
                   
                    Vector3 pos = plateuDeJeu.GetCentreTuile(piece.getColonne(), piece.getLigne());


                    if (piece.getCouleur() == 1)
                        piece3D = Instantiate(piecePrefabs[3], pos, piecePrefabs[3].transform.rotation);
                    else
                        piece3D = Instantiate(piecePrefabs[2], pos, piecePrefabs[2].transform.rotation);

                    pieces3D[piece.getColonne(), piece.getLigne()] = piece3D;
                    Debug.Log("Nouvelle Pièce");
               }
           
            
            
            if (piece is Tour)
            {
                piece3D.layer = LayerMask.NameToLayer("Tour");

                Vector3 pos = plateuDeJeu.GetCentreTuile(piece.getColonne(), piece.getLigne());

                if (piece.getCouleur() == 1)
                    piece3D = Instantiate(piecePrefabs[5], pos, piecePrefabs[5].transform.rotation);
                else
                    piece3D = Instantiate(piecePrefabs[4], pos, piecePrefabs[4].transform.rotation);

                pieces3D[piece.getColonne(), piece.getLigne()] = piece3D;
            }
            
            
            if (piece is Reine)
            {
                piece3D.layer = LayerMask.NameToLayer("Reine");

                Vector3 pos = plateuDeJeu.GetCentreTuile(piece.getColonne(), piece.getLigne());

                if (piece.getCouleur() == 1)
                    piece3D = Instantiate(piecePrefabs[6], pos, piecePrefabs[6].transform.rotation);
                else
                    piece3D = Instantiate(piecePrefabs[7], pos, piecePrefabs[7].transform.rotation);

                pieces3D[piece.getColonne(), piece.getLigne()] = piece3D;
            }
            
            
            if (piece is Fou)
            {
                piece3D.layer = LayerMask.NameToLayer("Fou");

                Vector3 pos = plateuDeJeu.GetCentreTuile(piece.getColonne(), piece.getLigne());

                if (piece.getCouleur() == 1)
                    piece3D = Instantiate(piecePrefabs[8], pos, piecePrefabs[8].transform.rotation);
                else
                    piece3D = Instantiate(piecePrefabs[9], pos, piecePrefabs[9].transform.rotation);

                pieces3D[piece.getColonne(), piece.getLigne()] = piece3D;
            }
            
            
            if (piece is Cavalier)
            {
                piece3D.layer = LayerMask.NameToLayer("Cavalier");

                Vector3 pos = plateuDeJeu.GetCentreTuile(piece.getColonne(), piece.getLigne());

                if (piece.getCouleur() == 1)
                    piece3D = Instantiate(piecePrefabs[10], pos, piecePrefabs[10].transform.rotation);
                else
                    piece3D = Instantiate(piecePrefabs[11], pos, piecePrefabs[11].transform.rotation);


                pieces3D[piece.getColonne(), piece.getLigne()] = piece3D;
            }

        }
    }

    public void deplacerPiece3D(int posiX, int posiY,int posfX, int posfY, int equipe)
    {
        GameObject copieObject = pieces3D[posiX, posiY];
        
        
        Vector3 pos = plateuDeJeu.GetCentreTuile(posfX, posfY);
        //Déplacer la piece vers sa nouvelle position
        if (pieces3D[posfX, posfY] == null)
        {
            pieces3D[posiX, posiY].transform.position = pos;
        }
        else
        {
            if (equipe == 1)
            {
                pieces3D[posfX, posfY].transform.position = plateuDeJeu.GetCentreTuile(nbreDePieceMangeB, -2);
                nbreDePieceMangeB++;
            }
            else
            {
                pieces3D[posfX, posfY].transform.position = plateuDeJeu.GetCentreTuile(nbreDePieceMangeN, 9);
                nbreDePieceMangeN++;
            }
            pieces3D[posfX, posfY] = null;
            pieces3D[posiX, posiY].transform.position = pos;
        }

        pieces3D[posfX, posfY] = copieObject;
        pieces3D[posiX, posiY] = null;

    }
}
