using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarNivel(string NombreNivel)
    {
        SceneManager.LoadScene(NombreNivel);
    }
    public void CerrarJuego()
    {
        Application.Quit();
        Debug.Log("Juego Cerrado"); 
    }

}
