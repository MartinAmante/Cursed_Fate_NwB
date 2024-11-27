using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---------Audio Clip---------")]
    public AudioClip MainMenu;
    public AudioClip MusicLevel;
    public AudioClip MenuUpDown;
    public AudioClip Door;
    public AudioClip Portal;
    public AudioClip Hit;

    private static AudioManager instance;

    private Dictionary<string, AudioClip> sceneMusicDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
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
        sceneMusicDict.Add("Menu", MainMenu);
        sceneMusicDict.Add("Mapa Uno", MusicLevel);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneMusicDict.ContainsKey(scene.name))
        {
            AudioClip clipToPlay = sceneMusicDict[scene.name];

            if (musicSource.clip != clipToPlay || !musicSource.isPlaying)
            {
                musicSource.clip = clipToPlay;
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("No music assigned for scene: " + scene.name);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}


