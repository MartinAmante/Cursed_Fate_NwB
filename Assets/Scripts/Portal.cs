using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int cantidadEnemigos;

    [SerializeField] private int enemigosEliminados;

    private Animator animator;
    void Start()
    {
        animator= GetComponent<Animator>();
        cantidadEnemigos = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void ActivarPortal()
    {
        animator.SetTrigger("Activar");
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
        if (other.CompareTag("Player") || enemigosEliminados == cantidadEnemigos)
            { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
