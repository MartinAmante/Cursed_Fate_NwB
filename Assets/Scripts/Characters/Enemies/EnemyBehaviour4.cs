using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour4 : MonoBehaviour
{
    [Header("Otros Parámetros")]
    public Transform jugador;
    public LayerMask capaJugador;
    private int movimiento;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;
    [SerializeField] private Enemy OGdata;
    [SerializeField] private EnemyData enemyData;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Accion();
    }

    void Update()
    {
        enemyData.IsAlert = Physics2D.OverlapCircle(transform.position, enemyData.DetectionRange, capaJugador);

        if (!enemyData.IsAlert)
        {
            ManejarMovimientoNormal();
            OGdata.chara.IsWaiting = true;
        }
        else
        {
            ManejarPersecucionJugador();
        }
    }

    void ManejarMovimientoNormal()
    {
        OGdata.chara.IsAttacking = false;
        OGdata.chara.IsWaiting = false;

        if (OGdata.chara.IsWalking)
        {
            rb.velocity = direccionMovimiento * OGdata.chara.MoveSpeed;
            OGdata.chara.IsWalking = true;
        }
        else if (OGdata.chara.IsWaiting)
        {
            rb.velocity = Vector2.zero;
            OGdata.chara.IsWalking = false;
        }
        else if (OGdata.chara.IsTurning)
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
        OGdata.chara.IsAttacking = false;
        OGdata.chara.IsWaiting = false;
        if (transform.position.x < jugador.transform.position.x)
        {
            OGdata.chara.IsTurning = false;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * OGdata.chara.RunSpeed;
            OGdata.chara.IsWalking = true;
        }
        else
        {
            OGdata.chara.IsTurning = true;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * OGdata.chara.RunSpeed;
            OGdata.chara.IsWalking = true;
        }
    }

    void AtacarJugador()
    {
        rb.velocity = Vector2.zero;
        OGdata.chara.IsWalking = false;
        OGdata.chara.IsAttacking = true;
    }
    void CambiarDireccion()
    {
        // Cambiar la dirección de movimiento a una dirección aleatoria
        direccionMovimiento = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = direccionMovimiento * OGdata.chara.MoveSpeed;
    }
    public void FinalAnimacion()
    {
        OGdata.chara.IsAttacking = false;
    }

    void Accion()
    {
        movimiento = Random.Range(1, 4);

        OGdata.chara.IsWalking = movimiento == 1;
        OGdata.chara.IsWaiting = movimiento == 2;
        OGdata.chara.IsTurning = movimiento == 3;

        if (OGdata.chara.IsTurning)
        {
            StartCoroutine(TiempoGiro());
        }

        Invoke("Accion", enemyData.ReactionTime);
    }

    IEnumerator TiempoGiro()
    {
        yield return new WaitForSeconds(2);
        OGdata.chara.IsTurning = false;
    }
    public void Morir()
    {
        OGdata.chara.IsWalking = false;
        OGdata.chara.IsAttacking = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
    }
}
