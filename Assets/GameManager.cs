using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text echiquier;
    Partie partie = new Partie();
    public InputField fli;
    public string li;
    public string ci;
    public string lf;
    public string cf;
    public int ili;
    public InputField inputField;
    string user;
    // Start is called before the first frame update
    void Start()
    {
      
        echiquier.text = partie.afficher();
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
            partie.jouerCoup(diviser[0], diviser[1], diviser[2], diviser[3], diviser[4]);
            echiquier.text = partie.afficher();
        }
    }
}
