using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public EnemyPatrol enemyPatrol;
    public DetectarEnemigo detectarEnemigo;
   

    // Start is called before the first frame update
    void Start()
    {
        detectarEnemigo.GetComponent<DetectarEnemigo>();
        enemyPatrol.GetComponent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPatrol.enabled == true )//|| detectarEnemigo == false)
        {
            detectarEnemigo.enabled = false;
            //enemyPatrol.enabled = false;
        }
    }
}
