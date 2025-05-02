using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
                                    

public class naviguerMenu : MonoBehaviour
{
    public void ChangerScene(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }

        
}
