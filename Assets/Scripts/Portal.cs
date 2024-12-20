
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int cantidadEnemigos;

    [SerializeField] private int enemigosEliminados;

    [SerializeField] private GameObject player;

    private Animator animator;
    private bool portalActivo = false;
    private Collider2D portalCollider;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        cantidadEnemigos = GameObject.FindGameObjectsWithTag("Enemy").Length;
        portalCollider = GetComponent<Collider2D>();
        portalCollider.enabled = false;
    }

    private void ActivarPortal()
    {
        if (SceneManager.GetActiveScene().name == "Mapa Tres")
        {
            player.SetActive(false);
            SceneManager.LoadScene("Menu");
        }
        else
        portalActivo = true;
        animator.SetTrigger("Activar");
        portalCollider.enabled = true;
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados += 1;

        if (enemigosEliminados == cantidadEnemigos)
        {
            ActivarPortal();
        }
    }

    public void IncrementarEnemigos()
    {
        cantidadEnemigos += 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || enemigosEliminados == cantidadEnemigos || portalActivo)
            { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

