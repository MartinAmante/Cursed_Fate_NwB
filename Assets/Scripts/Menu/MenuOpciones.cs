using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuOpciones : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;

    public void CambiarResolucion(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void SetResolution1280x720() { Screen.SetResolution(1280, 720, FullScreenMode.Windowed); }
    public void SetResolution1920x1080() { Screen.SetResolution(1920, 1080, FullScreenMode.Windowed); }
    public void SetResolution1440x900() { Screen.SetResolution(1440, 900, FullScreenMode.Windowed); }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void ReturnMenue(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
        Time.timeScale = 1;

    }

}
