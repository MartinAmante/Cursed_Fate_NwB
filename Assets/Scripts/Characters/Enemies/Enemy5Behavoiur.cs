using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy5Behavoiur : MonoBehaviour
{
    [Header("Parámetros de Movimiento")]
    public float velMovimiento;
    public float tiempoReaccion = 0.8f;
    public float velocidad = 3f;
    public float rangoAlerta;

    [Header("Estados de Movimiento")]
    public bool espera, camina, gira, estarAlerta;

    [Header("Otros Parámetros")]
    public Transform jugador;
    public LayerMask capaJugador;
    [SerializeField] private CharacterData enemy;
    private int movimiento;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Accion();
    }

    void Update()
    {
        estarAlerta = Physics2D.OverlapCircle(transform.position, rangoAlerta, capaJugador);

        if (!estarAlerta)
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

        if (camina)
        {
            rb.velocity = direccionMovimiento * velMovimiento;
            enemy.IsWalking = true;
        }
        else if (espera)
        {
            rb.velocity = Vector2.zero;
            enemy.IsWalking = false;
        }
        else if (gira)
        {
             CambiarDireccion();
        }
    }

    void ManejarPersecucionJugador()
    {
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);
        

        if (distanciaAlJugador > 8f)
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
            enemy.IsTurning = true;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * enemy.RunSpeed;
            enemy.IsWalking = true;
        }
        else
        {
            enemy.IsTurning = false;
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
        rb.velocity = direccionMovimiento * velMovimiento;
    }
    public void FinalAnimacion()
    {
        enemy.IsAttacking = false;
    }

    void Accion()
    {
        movimiento = Random.Range(1, 4);

        camina = movimiento == 1;
        espera = movimiento == 2;
        gira = movimiento == 3;

        if (gira)
        {
            StartCoroutine(TiempoGiro());
        }

        Invoke("Accion", tiempoReaccion);
    }

    IEnumerator TiempoGiro()
    {
        yield return new WaitForSeconds(2);
        gira = false;
    }
    public void Morir()
    {
        enemy.IsWalking = false;
        enemy.IsAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    }
}
