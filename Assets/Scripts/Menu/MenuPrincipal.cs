using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public Transform camara;
    public Transform player;
    public float duracion = 2f;
    public Animator puertaAnim;
    [SerializeField] public CharacterData PlayerData;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void IniciarAnimacion()
    {
        puertaAnim.SetTrigger("AbrirPuerta");

        StartCoroutine(MoverCamara());

        audioManager.PlaySFX(audioManager.Door);
    }

    IEnumerator MoverCamara()
    {
        Vector3 inicio = camara.position;
        Vector3 fin = player.position;

        float tiempo = 0;
        while (tiempo < duracion)
        {
            camara.position = Vector3.Lerp(inicio, fin, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }
        camara.position = fin;

        CambioNivel();

    }

    public void CambioNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerData.Lifes = 3;
        PlayerData.Health = PlayerData.MaxHealth;
    }

    public void OptionMenu(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);

    }

    public void CerrarJuego()
    {
        Application.Quit();
        Debug.Log("Juego Cerrado");
    }
}
