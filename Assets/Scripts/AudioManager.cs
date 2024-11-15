using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importa SceneManagement para gestionar escenas

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip---------")]
    public AudioClip MainMenu;
    public AudioClip MenuSelect;
    public AudioClip MenuUpDown;
    public AudioClip Door;
    public AudioClip Portal;

    private static AudioManager instance;

    private void Awake()
    {
        // Aseguramos que solo exista un AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evita que se destruya entre escenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        musicSource.clip = MainMenu;
        musicSource.Play();

        // Suscribimos un método para cuando cambie la escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Desuscribimos para evitar errores si el objeto se destruye
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Opciones")
        {
            // Aquí puedes ajustar el audio al entrar a Opciones
            // Ejemplo: musicSource.volume = 0.5f; o musicSource.Pause();
        }
        else if (scene.name == "Menu")
        {
            // Si vuelves al menú, puedes restaurar el audio como prefieras
            if (!musicSource.isPlaying)
            {
                musicSource.clip = MainMenu;
                musicSource.Play();
            }
        }
        else if (scene.name == "Mapa Uno") // Cambia "Nivel" por el nombre de tu escena de juego
        {
            // Detenemos la música al entrar al nivel
            musicSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}