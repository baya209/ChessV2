using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionnaireAudio : MonoBehaviour
{

    [Header("---------- Source Audio ----------------")]

    [SerializeField]
    AudioSource musiqueSource
       ;

    [Header("----------- Clip Audio-----------------")]
    public AudioClip musique;

    // Instance unique pour �viter la duplication du gestionnaire
    private static GestionnaireAudio instance;

    [Header("---------- Contr�le du Volume ----------------")]
    public Slider volumeSlider; // Slider pour le volume


    void Awake()
    {
        // V�rifie s'il existe d�j� une instance de GestionnaireAudio
        if (instance == null)
        {
            // Si ce n'est pas le cas, on d�finit cette instance comme la r�f�rence unique
            instance = this;

            // Ne d�truit pas cet objet lors du changement de sc�ne
            DontDestroyOnLoad(gameObject);

            // Initialise la source audio avec le clip de musique
            if (musiqueSource != null && musique != null)
            {
                musiqueSource.clip = musique;
                musiqueSource.loop = true;  // Fais en sorte que la musique se r�p�te
                musiqueSource.Play();  // Commence � jouer la musique
            }
        }
        else
        {
            // Si une instance existe d�j�, on d�truit ce GameObject
            Destroy(gameObject);
        }
        if (volumeSlider != null)
        {
            volumeSlider.value = musiqueSource.volume;  // D�finit la valeur initiale du volume
            volumeSlider.onValueChanged.AddListener(SetVolume);  // Ajoute l'�couteur pour le changement de valeur
        }
    }
    public void SetVolume(float volume)
    {
        musiqueSource.volume = volume;  // Change le volume de la musique en fonction du slider
    }

}
