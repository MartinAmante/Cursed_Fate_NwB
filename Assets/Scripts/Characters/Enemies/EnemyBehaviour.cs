using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform jugador;
    public LayerMask capaJugador;
    private int movimiento;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;
    [SerializeField] private Enemy golemData;
    [SerializeField] private EnemyData enemyData;


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
        }
        else
        {
            ManejarPersecucionJugador();
        }
    }

    void ManejarMovimientoNormal()
    {
        golemData.chara.IsAttacking = false;
        

        if (golemData.chara.IsWalking)
        {
            rb.velocity = direccionMovimiento * golemData.chara.MoveSpeed;
            golemData.chara.IsWalking = true;
        }
        else if (golemData.chara.IsWaiting)
        {
            rb.velocity = Vector2.zero;
            golemData.chara.IsWalking = false;
        }
        else if (golemData.chara.IsTurning)
        {
            //CambiarDireccion();   
        }
    }

    void ManejarPersecucionJugador()
    {
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);


        if (distanciaAlJugador > 5f)
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
        golemData.chara.IsAttacking = false;
        golemData.chara.IsWaiting = false;
        if (transform.position.x < jugador.transform.position.x)
        {
            golemData.chara.IsTurning = true;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * golemData.chara.RunSpeed;
            golemData.chara.IsWalking = true;
        }
        else
        {
            golemData.chara.IsTurning = false;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * golemData.chara.RunSpeed;
            golemData.chara.IsWalking = true;
        }
    }

    void AtacarJugador()
    {
        rb.velocity = Vector2.zero;
        golemData.chara.IsAttacking = true;
        golemData.chara.IsWalking = false;
    }
    void CambiarDireccion()
    {
        // Cambiar la dirección de movimiento a una dirección aleatoria
        direccionMovimiento = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = direccionMovimiento * golemData.chara.MoveSpeed;
    }
    public void FinalAnimacion()
    {
        golemData.chara.IsAttacking = false;
    }

    void Accion()
    {
        movimiento = Random.Range(1, 3);
        golemData.chara.IsWalking = movimiento == 1;
        golemData.chara.IsWaiting = movimiento == 2;
        Invoke("Accion", enemyData.ReactionTime);
    }
    public void Mrir()
    {
        golemData.chara.IsAttacking = false;
        golemData.chara.IsWalking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
    }
}
