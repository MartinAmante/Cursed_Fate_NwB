using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerte : MonoBehaviour
{
    [SerializeField] public CharacterData PlayerData;
    public void RestartButton()
    {
        // Obtener el último nivel guardado y cargarlo
        string lastLevel = PlayerPrefs.GetString("LastLevel", "DefaultSceneName"); // "Nivel1" es un valor por defecto
        SceneManager.LoadScene(lastLevel);
        PlayerData.IsAlive = true;
        PlayerData.Lifes = 3;
        PlayerData.Health = PlayerData.MaxHealth;

    }

    public void ReturnMenu(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
        Time.timeScale = 1;
        PlayerData.IsAlive = true;
        PlayerData.Lifes = 3;
        PlayerData.Health = PlayerData.MaxHealth;
    }
}
