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
    public float DistanciaAtaque;
    [SerializeField] private Enemy seekerData;
    [SerializeField] private EnemyData enemyData;
    //private int movimiento;
    //private Animator animator;
    private Rigidbody2D rb;
    private Vector2 direccionMovimiento;
    public bool tiempoEspera = true;


    void Start()
    {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Accion();

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
        seekerData.chara.IsAttacking = false;
        seekerData.chara.IsWalking = false;

        if (seekerData.chara.IsWaiting)
        {
            rb.velocity = Vector2.zero;
            seekerData.chara.IsWalking = false;
        }
        else if (seekerData.chara.IsTurning)
        {
            CambiarDireccion();
        }
    }

    void ManejarPersecucionJugador()
    {
        if (tiempoEspera)
        {
            enemyData.IsSpawning = true;
            seekerData.chara.IsWaiting = true;
            StartCoroutine(TiempoEspera());
            

        }
        else
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
    }
    IEnumerator TiempoEspera()
    {
        yield return new WaitForSeconds(enemyData.WaitTime);
        tiempoEspera = false;
        //enemyData.IsSpawning = false;
    }
    void PerseguirJugador()
    {
        seekerData.chara.IsAttacking = false;
        seekerData.chara.IsWaiting = false;
        if (transform.position.x < jugador.transform.position.x)
        {
            seekerData.chara.IsTurning = false;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * seekerData.chara.RunSpeed;
            seekerData.chara.IsWalking = true;
        }
        else
        {
            seekerData.chara.IsTurning = true;
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb.velocity = direccion * seekerData.chara.RunSpeed;
            seekerData.chara.IsWalking = true;
        }
    }

    void AtacarJugador()
    {
        rb.velocity = Vector2.zero;
        seekerData.chara.IsWalking = false;
        seekerData.chara.IsWaiting = false;
        seekerData.chara.IsAttacking = true;
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
