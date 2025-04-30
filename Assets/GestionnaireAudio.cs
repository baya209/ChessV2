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

    // Instance unique pour éviter la duplication du gestionnaire
    private static GestionnaireAudio instance;

    [Header("---------- Contrôle du Volume ----------------")]
    public Slider volumeSlider; // Slider pour le volume


    void Awake()
    {
        // Vérifie s'il existe déjà une instance de GestionnaireAudio
        if (instance == null)
        {
            // Si ce n'est pas le cas, on définit cette instance comme la référence unique
            instance = this;

            // Ne détruit pas cet objet lors du changement de scène
            DontDestroyOnLoad(gameObject);

            // Initialise la source audio avec le clip de musique
            if (musiqueSource != null && musique != null)
            {
                musiqueSource.clip = musique;
                musiqueSource.loop = true;  // Fais en sorte que la musique se répète
                musiqueSource.Play();  // Commence à jouer la musique
            }
        }
        else
        {
            // Si une instance existe déjà, on détruit ce GameObject
            Destroy(gameObject);
        }
        if (volumeSlider != null)
        {
            volumeSlider.value = musiqueSource.volume;  // Définit la valeur initiale du volume
            volumeSlider.onValueChanged.AddListener(SetVolume);  // Ajoute l'écouteur pour le changement de valeur
        }
    }
    public void SetVolume(float volume)
    {
        musiqueSource.volume = volume;  // Change le volume de la musique en fonction du slider
    }

}
