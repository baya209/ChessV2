
using UnityEngine;

public class PlateuDeJeu : MonoBehaviour
{
  
    private GameObject[,] plateauJeu;
    private const int TAILLE_X = 8; // Dimension du du plateau (8 cases par 8 cases)
    private const int TAILLE_Y = 8; // Dimension du du plateau (8 cases par 8 cases)
    private Camera cameraActuelle;
    private Vector2Int positionSurvol; // Position de survol de la souris

    private Vector3 bonds; // D�calage pour trouver le centre des cases, toutes les cases sont de m�me tailles

    [Header("material")]
    [SerializeField] private Material[] material; 
    


    
    [SerializeField] private float taillesX = 10f; //Dimension de la case/tuile dans unity    
    [SerializeField] private float taillesY = 0.15f;// Decalage du plateau virtuelle (un peu vers le hat) permettant d'interagir avec les pi�ces
    [SerializeField] private Vector3 vec = Vector3.zero;



    private void Awake()
    {
        creerPlateauJeu(taillesX, TAILLE_X, TAILLE_Y);

    }

    private void Update()
    {

        if (!cameraActuelle)
        {
            cameraActuelle = Camera.main;
            return;
        }

        RaycastHit info;
        Ray ray = cameraActuelle.ScreenPointToRay(Input.mousePosition);

        //Si la souris est par dessus une tuile
        if (Physics.Raycast(ray, out info, 1000, LayerMask.GetMask("Tuile","Survol"))) {

            Debug.Log("Sur une pi�ce");
            Vector2Int hitPosition = trouverIndexTuile(info.transform.gameObject);

            if(positionSurvol == -Vector2Int.one)
            {
                reinitialiserCouelurCasue();


                positionSurvol = hitPosition;
                plateauJeu[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Survol");
                plateauJeu[hitPosition.x, hitPosition.y].GetComponent<MeshRenderer>().material = material[1];
            }

            if (positionSurvol != hitPosition)
            {
                reinitialiserCouelurCasue();

                plateauJeu[positionSurvol.x,positionSurvol.y].layer = LayerMask.NameToLayer("Tuile"); 
                positionSurvol = hitPosition;
                plateauJeu[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Survol");
                plateauJeu[hitPosition.x, hitPosition.y].GetComponent<MeshRenderer>().material = material[1];


            }

        }
        else {
            if (positionSurvol != -Vector2Int.one){
                // Si la souris ne se trouve sur aucune case valide ( on r�initialise la couleur ) on ne survole donc plus la case
                
                reinitialiserCouelurCasue();
                plateauJeu[positionSurvol.x, positionSurvol.y].layer = LayerMask.NameToLayer("Tuile");
                positionSurvol = -Vector2Int.one;

                
               
                Debug.Log("Pas sur une pi�ce");
               }
            
        }
        Debug.Log(positionSurvol.ToString());
    }


    private void creerPlateauJeu(float taille , int tailleX, int tailleY)
    {
        taillesY += transform.position.y;
        bonds = new Vector3((tailleX / 2) * taille, 0, (tailleX / 2) * taille) + vec;
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

        // Creation d'un contenant pour le 3d 
        Mesh mesh = new Mesh();
        cases.AddComponent<MeshFilter>().mesh = mesh;
        cases.AddComponent<MeshRenderer>().material = material[0]; 
        

        // Car notre rectangle pour la case a 4 cotes

        Vector3[] vecteur = new Vector3[4];
        vecteur[0] = new Vector3(i * taille, taillesY, j * taille) - bonds;        // coin bas gauche
        vecteur[1] = new Vector3((i + 1) * taille, taillesY, j * taille) - bonds;   // coin bas droit
        vecteur[2] = new Vector3(i * taille, taillesY, (j + 1) * taille) - bonds;   // coin haut gauche
        vecteur[3] = new Vector3((i + 1) * taille, taillesY, (j + 1) * taille) - bonds; // coin haut droit

        int[] rectangle = new int[6] { 0, 2, 1, 1, 2, 3 };
        mesh.vertices = vecteur;
        mesh.triangles = rectangle;

        mesh.RecalculateNormals();

        cases.layer = LayerMask.NameToLayer("Tuile");
        cases.AddComponent<BoxCollider>();
       


        return cases;
    }
    

    private Vector2Int trouverIndexTuile(GameObject hitInfo)
    {
        for (int i = 0; i < TAILLE_X; i++)
        {
            for (int j = 0; j < TAILLE_Y; j++)
            {
                if (plateauJeu[i,j] == hitInfo)
                {
                    return new Vector2Int(i, j);
                }
            }
        }
        // Dans le cas ou l� souris ne clique pas sur une case de valide (qui ne devrait pas exister
        return new Vector2Int(-1, -1);    
    }
    
    public GameObject[,] GetPlateauJeu()
    {
        return plateauJeu;
    }
    public float GetTailleTuile()
    {
        return taillesX;
    }
    public float GetDecalageVersHaut()
    {
        return taillesY;
    }

    public Vector3 GetBounds()
    {
        return bonds;
    } 

    public Vector3 GetCentreTuile(float posX, float posY)
    {
        return new Vector3(posX * taillesX, taillesY, posY*taillesX) - bonds + new Vector3(taillesX/2,0,taillesX/2);
    }
    
    public void reinitialiserCouelurCasue()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                plateauJeu[i, j].GetComponent<MeshRenderer>().material = material[0];
            }
        }
    }

    public int[] GetCaseCliquee()
    {
        int[] coordCase = new int[2];
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit info;
            Ray ray = cameraActuelle.ScreenPointToRay(Input.mousePosition);

            if ((Physics.Raycast(ray, out info, 1000, LayerMask.GetMask("Tuile", "Survol")))) {
                Vector2Int hitPosition = trouverIndexTuile(info.transform.gameObject);
                coordCase[0] = hitPosition.x;
                coordCase[1] = hitPosition.y;
            }
            
            return coordCase;

        }
        return null;
    }

    public void changerCouleurCase(Coup coup, int indexMaterielDepart, int indexMaterielArrivee)
    {
        
            plateauJeu[coup.getCi(), coup.getLi()].GetComponent<MeshRenderer>().material = material[indexMaterielDepart];
            plateauJeu[coup.getCf(), coup.getLf()].GetComponent<MeshRenderer>().material = material[indexMaterielArrivee];
        
    }
}
