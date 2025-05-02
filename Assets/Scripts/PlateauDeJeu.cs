
using UnityEngine;

public class PlateuDeJeu : MonoBehaviour
{
  
    private GameObject[,] plateauJeu;
    private const int TAILLE_X = 8;
    private const int TAILLE_Y = 8;
    [Header("material")]
    [SerializeField] private Material material;




    private void Awake()
    {
        creerPlateauJeu(1, TAILLE_X, TAILLE_Y);

    }


    private void creerPlateauJeu(float taille , int tailleX, int tailleY)
    {
        plateauJeu = new GameObject[tailleX, tailleY];

        for (int i = 0; i < tailleX; i++)
        {
            for (int j = 0; j < tailleY; j++)
            {
                plateauJeu[i,j] = genererUneCase(taille, i, j);
            }
        }
           
    }

     

    private GameObject genererUneCase(float taille, int i, int j)
    {
        GameObject cases = new GameObject(string.Format("X: {0}, Y: {1}", i, j));
        cases.transform.parent = transform;

        // creation d'un contenant pour le 3d 
        Mesh mesh = new Mesh();
        cases.AddComponent<MeshFilter>().mesh = mesh;
        cases.AddComponent<MeshRenderer>().material = material;
        

        // car notre rectangle pour la case a 4 cotes

        Vector3[] vecteur = new Vector3[4];
        vecteur[0] = new Vector3(i * taille, 0, j * taille);         // coin bas gauche
        vecteur[1] = new Vector3((i + 1) * taille, 0, j * taille);   // coin bas droit
        vecteur[2] = new Vector3(i * taille, 0, (j + 1) * taille);   // coin haut gauche
        vecteur[3] = new Vector3((i + 1) * taille, 0, (j + 1) * taille); // coin haut droit

        int[] rectangle = new int[6] { 0, 2, 1, 1, 2, 3 };
        mesh.vertices = vecteur;
        mesh.triangles = rectangle;
        mesh.RecalculateNormals();


        cases.AddComponent<BoxCollider>();



        return cases;
    }
    
    
    
}
