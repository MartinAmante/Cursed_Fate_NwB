using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoroutine : MonoBehaviour
{
    public Transform enemigo;
    public Transform player;
    public Transform[] puntosDeControl;
    public Vector3 playerPosition;
    public Vector3 ubicacionInicial;
    public Quaternion rotacionInicial;
    public float velocidad = 5f;
    public bool activado;
    public GameObject BalaInicio;
    public GameObject BalaPrefab;
    public float BalaVelocidad;
    public bool enemigoPatrullando;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RealizarPatrullaje");
        ubicacionInicial = enemigo.GetComponent<Transform>().position;
        rotacionInicial = enemigo.GetComponent<Transform>().rotation;

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.position.x, enemigo.position.y, player.position.z);
        if (enemigoPatrullando == false)
        {
            enemigo.transform.position = Vector3.MoveTowards(transform.position, playerPosition, velocidad * Time.deltaTime);
            enemigo.transform.LookAt(playerPosition);
        }
    }


    IEnumerator RealizarPatrullaje()
    {
        
        int i = 0;
        Vector3 nuevaPosicion = new Vector3(puntosDeControl[i].position.x, enemigo.transform.position.y, puntosDeControl[i].position.z);
        while (true)
        {
            enemigoPatrullando = true;
            while (enemigo.transform.position != nuevaPosicion)
            {
                enemigo.transform.position = Vector3.MoveTowards(enemigo.transform.position, nuevaPosicion, velocidad * Time.deltaTime);
                enemigo.transform.LookAt(nuevaPosicion);
                
                yield return null;
                
            }
            //yield return StartCoroutine("RotarEnemigo");
            if (i < puntosDeControl.Length - 1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
            nuevaPosicion = new Vector3(puntosDeControl[i].position.x, enemigo.transform.position.y, puntosDeControl[i].position.z);
            Debug.Log(i);
        }
    }
    /*IEnumerator PerseguirEnemigo()
    {
        while (activado == true)
        {
            enemigo.transform.position = Vector3.MoveTowards(transform.position, playerPosition, velocidad * Time.deltaTime);
            enemigo.transform.LookAt(playerPosition);
            enemigoPatrullando = false;
        }
        return null;
    }*/

     private void OnTriggerEnter(Collider other)
     {
         if (other.tag == "Player")
         {
             activado = true;
             InvokeRepeating("Disparar", 0, 1f);
             enemigoPatrullando = false;
             StopCoroutine("RealizarPatrullaje");
            //StartCoroutine("PerseguirEnemigo");

         }
     }
     private void OnTriggerExit(Collider other)
     {
         if (other.tag == "Player")
         {
             activado = false;
             CancelInvoke("Disparar");
            //Invoke("RegresarPosicion", 0);
           // StopCoroutine("PerseguirEnemigo");
            StartCoroutine("RealizarPatrullaje");
         }
     }
     private void Disparar()
     {
         GameObject BalaTemporal = Instantiate(BalaPrefab, BalaInicio.transform.position, BalaInicio.transform.rotation);
         Rigidbody rb = BalaTemporal.GetComponent<Rigidbody>();
         rb.AddForce(transform.forward * BalaVelocidad);
         Destroy(BalaTemporal, 5f);
     }
     /*private void RegresarPosicion()
     {
         Debug.Log("tuki");
         enemigo.transform.position = Vector3.MoveTowards(transform.position, ubicacionInicial, velocidad * Time.deltaTime);
         enemigo.transform.LookAt(ubicacionInicial);
         if (enemigo.transform.position == ubicacionInicial)
         {
             Invoke("RegresarRotacion", 0);
             //StartCoroutine("RealizarPatrullaje");
         }
     }
     private void RegresarRotacion()
     {
         enemigo.transform.rotation = rotacionInicial;
     }*/




    /*IEnumerator RotarEnemigo()
    {
        yield return new WaitForSecondsRealtime(2);
        for (int i = 1; i <= 90; i++)
        {
            enemigo.transform.LookAt(puntosDeControl[i]);
        }
        yield return null;

    }
    */

}  
