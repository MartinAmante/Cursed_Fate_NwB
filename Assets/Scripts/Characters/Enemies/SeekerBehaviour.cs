using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerBehaviour : MonoBehaviour
{
    [Header("Parámetros de Movimiento")]
    public float velMovimiento;
    public float velocidad = 3f;
    [Header("Otros Parámetros")]
    public Transform jugador;
    public LayerMask capaJugador;
    [SerializeField] private CharacterData enemy;
    [SerializeField] private EnemyData enemyData;
    private int movimiento;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;
    public bool tiempoEspera = true;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Accion();
    }

    void Update()
    {
        enemyData.IsAlert = Physics2D.OverlapCircle(transform.position, enemyData.DetectionRange, capaJugador);

        if (!enemyData.IsAlert)
        {
            ManejarMovimientoNormal();
        }
        else
        {
            ManejarPersecucionJugador();
        }
    }

    void ManejarMovimientoNormal()
    {
        enemy.IsAttacking = false;
        enemy.IsWalking = false;

        if (enemy.IsWaiting)
        {
            rb.velocity = Vector2.zero;
            enemy.IsWalking = false;
        }
        else if (enemy.IsTurning)
        {
            CambiarDireccion();
        }
    }

    void ManejarPersecucionJugador()
    {
        if (tiempoEspera)
        {
            enemyData.IsSpawning = true;
            enemy.IsWaiting = true;
            StartCoroutine(TiempoEspera());
            

        }
        else
        {
            
            
            float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);


            if (distanciaAlJugador > 2f)
            {
                PerseguirJugador();
            }
            else
            {
                AtacarJugador();
            }
        }
    }
    IEnumerator TiempoEspera()
    {
        yield return new WaitForSeconds(enemyData.WaitTime);
        tiempoEspera = false;
        enemyData.IsSpawning = false;
    }
    void PerseguirJugador()
    {
        enemy.IsAttacking = false;
        enemy.IsWaiting = false;
        if (transform.position.x < jugador.transform.position.x)
        {
            enemy.IsTurning = false;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * enemy.RunSpeed;
            enemy.IsWalking = true;
        }
        else
        {
            enemy.IsTurning = true;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * enemy.RunSpeed;
            enemy.IsWalking = true;
        }
    }

    void AtacarJugador()
    {
        rb.velocity = Vector2.zero;
        enemy.IsWalking = false;
        enemy.IsWaiting = false;
        enemy.IsAttacking = true;
    }
    void CambiarDireccion()
    {
        // Cambiar la dirección de movimiento a una dirección aleatoria
        direccionMovimiento = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = direccionMovimiento * velMovimiento;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
    }
}
