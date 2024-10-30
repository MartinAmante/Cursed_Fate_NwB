using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject PauseMenue;

    public GameObject SalirMenue;

    public bool Pause = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause == false)
            {
                PauseMenue.SetActive(true);
                Pause = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;

                AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
                for (int i = 0; i < sonidos.Length; i++)
                {
                    sonidos[i].Pause();
                }
            }
            else if (Pause == true)
            {
                Resume();
            }
        }
        
    }
    public void Resume()
    {
        PauseMenue.SetActive(false);
        SalirMenue.SetActive(false);
        Pause = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sonidos.Length; i++)
        {
            sonidos[i].Play();
        }
    }
    public void ReturnMenue(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
    }
}
