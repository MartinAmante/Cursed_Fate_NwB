using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Enemy2Behaviour : MonoBehaviour
{
    [Header("Otros Parámetros")]
    public Transform jugador;
    public LayerMask capaJugador;
    public float DistanciaAtaque;
    [SerializeField] private Enemy sproutData;
    [SerializeField] private EnemyData enemyData;
    private int movimiento;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Accion();
        sproutData = gameObject.GetComponent<Enemy>();

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
            sproutData.chara.IsWaiting = true;
            sproutData.chara.IsWalking = false;
        }
        else
        {
            ManejarPersecucionJugador();
        }
    }

    void ManejarMovimientoNormal()
    {
        sproutData.chara.IsAttacking = false;

        if (sproutData.chara.IsWaiting)
        {
            rb.velocity = Vector2.zero;
            sproutData.chara.IsWalking = false;
        }
        else if (sproutData.chara.IsTurning)
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
        sproutData.chara.IsAttacking = false;
        sproutData.chara.IsWaiting = false;
        if (transform.position.x < jugador.transform.position.x)
        {
            sproutData.chara.IsTurning = false;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * sproutData.chara.RunSpeed;
            sproutData.chara.IsWalking = true;
        }
        else
        {
            sproutData.chara.IsTurning = true;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * sproutData.chara.RunSpeed;
            sproutData.chara.IsWalking = true;
        }
    }

    void AtacarJugador()
    {
        rb.velocity = Vector2.zero;
        sproutData.chara    .IsWalking = false;
        sproutData.chara.IsAttacking = true;
    }
    void CambiarDireccion()
    {
        // Cambiar la dirección de movimiento a una dirección aleatoria
        direccionMovimiento = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = direccionMovimiento * sproutData.chara.NormalSpeed;
    }
    public void FinalAnimacion()
    {
        sproutData.chara.IsAttacking = false;
    }

    void Accion()
    {
        movimiento = Random.Range(1, 2);

        //enemy.IsWalking = movimiento == 1;
        sproutData.chara.IsWaiting = movimiento == 1;
        sproutData.chara.IsTurning = movimiento == 2;

        if (sproutData.chara.IsTurning)
        {
            StartCoroutine(TiempoGiro());
        }

        Invoke("Accion", enemyData.ReactionTime);
    }

    IEnumerator TiempoGiro()
    {
        yield return new WaitForSeconds(2);
        sproutData.chara.IsTurning = false;
    }
    public void Morir()
    {
        sproutData.chara.IsWalking = false;
        sproutData.chara.IsAttacking = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
    }
}
