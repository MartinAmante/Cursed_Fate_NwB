using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Behaviour : MonoBehaviour
{
    [Header("Otros Parámetros")]
    public Transform jugador;
    public LayerMask capaJugador;
    private int movimiento;
    public float DistanciaAtaque;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;
    [SerializeField] private Enemy skeletonData;
    [SerializeField] private EnemyData enemyData;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Accion();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            jugador = playerObject.transform;
        }
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
        skeletonData.chara.IsAttacking = false;

        if (skeletonData.chara.IsWalking)
        {
            rb.velocity = direccionMovimiento * skeletonData.chara.MoveSpeed;
            skeletonData.chara.IsWalking = true;
        }
        else if (skeletonData.chara.IsWaiting)
        {
            rb.velocity = Vector2.zero;
            skeletonData.chara.IsWalking = false;
        }
        else if (skeletonData.chara.IsTurning)
        {
            CambiarDireccion();
        }
    }

    void ManejarPersecucionJugador()
    {
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);


        if (distanciaAlJugador > DistanciaAtaque)
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
        skeletonData.chara.IsAttacking = false;
        skeletonData.chara.IsWaiting = false;
        if (transform.position.x < jugador.transform.position.x)
        {
            skeletonData.chara.IsTurning = false;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * skeletonData.chara.RunSpeed;
            skeletonData.chara.IsWalking = true;
        }
        else
        {
            skeletonData.chara.IsTurning = true;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * skeletonData.chara.RunSpeed;
            skeletonData.chara.IsWalking = true;
        }
    }

    void AtacarJugador()
    {
        rb.velocity = Vector2.zero;
        skeletonData.chara.IsWalking = false;
        skeletonData.chara.IsAttacking = true;
    }
    void CambiarDireccion()
    {
        // Cambiar la dirección de movimiento a una dirección aleatoria
        direccionMovimiento = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = direccionMovimiento * skeletonData.chara.MoveSpeed;
    }
    public void FinalAnimacion()
    {
        skeletonData.chara.IsAttacking = false;
    }

    void Accion()
    {
        movimiento = Random.Range(1, 4);

        skeletonData.chara.IsWalking = movimiento == 1;
        skeletonData.chara.IsWaiting = movimiento == 2;
        skeletonData.chara.IsTurning = movimiento == 3;

        if (skeletonData.chara.IsTurning)
        {
            StartCoroutine(TiempoGiro());
        }

        Invoke("Accion", enemyData.ReactionTime);
    }

    IEnumerator TiempoGiro()
    {
        yield return new WaitForSeconds(2);
        skeletonData.chara.IsTurning = false;
    }
    public void Morir()
    {
        skeletonData.chara.IsWalking = false;
        skeletonData.chara.IsAttacking = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
    }
}
