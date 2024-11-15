using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behaviour : MonoBehaviour
{
    [Header("Otros Parámetros")]
    public Transform jugador;
    public LayerMask capaJugador;
    [SerializeField] private CharacterData enemy;
    [SerializeField] private EnemyData enemyData;
    private int movimiento;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Accion();
    }

    void Update()
    {
        enemyData.IsAlert = Physics2D.OverlapCircle(transform.position, enemyData.DetectionRange, capaJugador);

        if (!enemyData.IsAlert)
        {
            ManejarMovimientoNormal();
            enemy.IsWaiting = true;
            enemy.IsWalking = false;
        }
        else
        {
            ManejarPersecucionJugador();
        }
    }

    void ManejarMovimientoNormal()
    {
        enemy.IsAttacking = false;

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
    }
    void CambiarDireccion()
    {
        // Cambiar la dirección de movimiento a una dirección aleatoria
        direccionMovimiento = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = direccionMovimiento * enemy.NormalSpeed;
    }
    public void FinalAnimacion()
    {
        enemy.IsAttacking = false;
    }

    void Accion()
    {
        movimiento = Random.Range(1, 2);

        //enemy.IsWalking = movimiento == 1;
        enemy.IsWaiting = movimiento == 1;
        enemy.IsTurning = movimiento == 2;

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
