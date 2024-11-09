using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaAnimacion : MonoBehaviour
{
    public Animator puertaAnim;
    public void IniciarAnimacion()
    {
        puertaAnim.SetTrigger("Abrir");

        StartCoroutine(MoverCamara());
    }

    public Transform camara;
    public Transform player;
    public float duracion = 2f;

    IEnumerator MoverCamara()
    {
        Vector3 inicio = camara.position;
        Vector3 fin = player.position;

        float tiempo = 0;
        while (tiempo < duracion)
        {
            camara.position = Vector3.Lerp(inicio, fin, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        camara.position = fin;

    }
}
