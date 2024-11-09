using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyTypes;

    [SerializeField]
    private float initTime;

    [SerializeField]
    private float repeatTime;

    [SerializeField]
    private int maxEnemies = 10; // Número máximo de enemigos a generar

    private int spawnCount = 0; // Contador de enemigos generados
    private IEnumerator couroutine;
    private Portal portal;

    void Start()
    {
        portal = GameObject.FindGameObjectWithTag("Portal").GetComponent<Portal>();
        couroutine = Enemies(4.0f);
        StartCoroutine(couroutine);
        // InvokeRepeating("GenerateEnemy", initTime, repeatTime);
    }

    private IEnumerator Enemies(float waitTime)
    {
        while (spawnCount < maxEnemies) // Mientras no se haya alcanzado el límite
        {
            yield return new WaitForSeconds(waitTime);

            int randomIndex = Random.Range(0, enemyTypes.Length);
            Instantiate(enemyTypes[randomIndex], transform.position, transform.rotation);

            spawnCount++; // Incrementar el contador de enemigos

            if (portal != null)
            {
                portal.IncrementarEnemigos();
            }

        }

        // Opcional: Desactiva el objeto o realiza alguna acción cuando se detenga el spawn
        gameObject.SetActive(false); // Desactivar el objeto que contiene el script
    }
}
