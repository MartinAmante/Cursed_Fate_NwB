using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public float speed = 1.5f;  // Velocidad de movimiento del enemigo

    private Transform playerTransform;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        // Mueve al enemigo hacia el jugador
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el enemigo toca al jugador
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Portal").GetComponent<Portal>().EnemigoEliminado();
            //Destroy(gameObject); // Destruye al enemigo
        }
    }
}
