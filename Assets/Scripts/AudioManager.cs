using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip---------")]
    public AudioClip MainMenu;
    public AudioClip MusicLevel;
    public AudioClip MenuUpDown;
    public AudioClip Door;
    public AudioClip Portal;
    public AudioClip Hit;

    private static AudioManager instance;

    private void Awake()
    {
        // Aseguramos que solo exista un AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Menu":
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = MainMenu;
                    musicSource.Play();
                }
                break;

            case "Mapa Uno":
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = MusicLevel;
                    musicSource.Play();
                }
                break;

            default:
                // Opcional: manejar casos no previstos
                break;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}