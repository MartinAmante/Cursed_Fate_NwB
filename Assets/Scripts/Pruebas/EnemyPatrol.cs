using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 5;
    public DetectarEnemigo detectarEnemigo;
    public Transform enemigo;
    public Transform[] puntosDeControl;
    public float velocidad = 5f;
    public bool activado;
   
    // Start is called before the first frame update
    void Start()
    {
        detectarEnemigo.GetComponent<DetectarEnemigo>();
        StartCoroutine("RealizarPatrullaje");
    }

    // Update is called once per frame
    void Update()
    {

        //moveEnemy();
        


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerFinal")
        {
            Debug.Log("Char Hit");
            activado = true;
            detectarEnemigo.enabled = true;
            StopCoroutine("RealizarPatrullaje");  
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerFinal" )
        {
                activado = false;
        }
    }
    IEnumerator RealizarPatrullaje()
    {

        int i = 0;
        Vector3 nuevaPosicion = new Vector3(puntosDeControl[i].position.x, enemigo.transform.position.y, puntosDeControl[i].position.z);
        while (true)
        {
            //enemigoPatrullando = true;
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
            //Debug.Log(i);
        }
    }
    /*void moveEnemy()
    {
        Enemigo.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        if (Enemigo.transform.position.z >= -260)
        {
            speed *= -1;
            Enemigo.transform.Rotate(0, 180, 0);
        }
        if (Enemigo.transform.position.z <= -290)
        {
            speed *= -1;
            Enemigo.transform.Rotate(0, 180, 0);
        }
    }*/

}
