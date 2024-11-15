using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Behaviour : MonoBehaviour
{
    [Header("Otros Par�metros")]
    public Transform jugador;
    public LayerMask capaJugador;
    private int movimiento;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;
    public AudioSource controlSonido;
    public AudioClip attackSound;
    [SerializeField] private CharacterData enemy;
    [SerializeField] private EnemyData enemyData;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Accion();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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

        if (enemy.IsWalking)
        {
            rb.velocity = direccionMovimiento * enemy.MoveSpeed;
            enemy.IsWalking = true;
        }
        else if (enemy.IsWaiting)
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
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);


        if (distanciaAlJugador > 4f)
        {
            PerseguirJugador();
        }
        else
        {
            AtacarJugador();
        }
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
        enemy.IsAttacking = true;
        if(enemy.IsAttacking && !controlSonido.isPlaying) controlSonido.PlayOneShot(attackSound);
    }
    void CambiarDireccion()
    {
        // Cambiar la direcci�n de movimiento a una direcci�n aleatoria
        direccionMovimiento = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = direccionMovimiento * enemy.MoveSpeed;
    }
    public void FinalAnimacion()
    {
        enemy.IsAttacking = false;
    }

    void Accion()
    {
        movimiento = Random.Range(1, 4);

        enemy.IsWalking = movimiento == 1;
        enemy.IsWaiting = movimiento == 2;
        enemy.IsTurning = movimiento == 3;

        if (enemy.IsTurning)
        {
            StartCoroutine(TiempoGiro());
        }

        Invoke("Accion", enemyData.ReactionTime);
    }

    IEnumerator TiempoGiro()
    {
        yield return new WaitForSeconds(2);
        enemy.IsTurning = false;
    }
    public void Morir()
    {
        enemy.IsWalking = false;
        enemy.IsAttacking = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
    }
}
