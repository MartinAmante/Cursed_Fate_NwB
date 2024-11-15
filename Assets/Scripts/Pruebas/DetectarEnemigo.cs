using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarEnemigo : MonoBehaviour
{
    public Transform enemigo;
    public Transform player;
    public float velocidadMovimiento = 4f;
    public bool activado;
    private Vector3 playerPosition;
    public GameObject BalaInicio;
    public GameObject BalaPrefab;
    public float BalaVelocidad;
    public Vector3 UbicacionInicial;
    public Quaternion RotacionInicial;
    //public float speed = 2;
    public EnemyPatrol enemyPatrol;

    private void Start()
    {
        UbicacionInicial = enemigo.GetComponent<Transform>().position;
        RotacionInicial = enemigo.GetComponent<Transform>().rotation;
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    private void Update()
    {
        
        playerPosition = new Vector3(player.position.x, enemigo.position.y, player.position.z - 7);
        if (activado == true)
        {
            enemigo.transform.position = Vector3.MoveTowards(transform.position, playerPosition, velocidadMovimiento * Time.deltaTime);
            enemigo.transform.LookAt(player);
            

        }
        if (activado == false)
        {
            Invoke("RegresarPosicion", 0);
            
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "PlayerFinal")
        {
                activado = true;
                InvokeRepeating("Disparar", 0, 1f);
                enemyPatrol.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerFinal")
        {
                activado = false;
                CancelInvoke("Disparar");
        }
    }

    private void Disparar()
    {
        GameObject BalaTemporal = Instantiate(BalaPrefab, BalaInicio.transform.position , BalaInicio.transform.rotation);
        Rigidbody rb = BalaTemporal.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * BalaVelocidad);
        Destroy(BalaTemporal, 5f);
    }
    private void RegresarPosicion()
    {
        enemigo.transform.position = Vector3.MoveTowards(transform.position, UbicacionInicial , velocidadMovimiento * Time.deltaTime);
        enemigo.transform.LookAt(UbicacionInicial);
       if (enemigo.transform.position == UbicacionInicial)
        {
            Invoke("RegresarRotacion",0);
            enemyPatrol.enabled = true;
            enemyPatrol.StartCoroutine("RealizarPatrullaje");
        }
    }
    private void RegresarRotacion()
    {
        enemigo.transform.rotation = RotacionInicial;
    }
   
        
    

}
