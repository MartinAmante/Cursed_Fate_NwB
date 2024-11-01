using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character{

    [SerializeField] private EnemyData enemyData;
    public AudioSource controlSonido;
    public AudioClip deathSound;

    public override void CheckHealth(){
        if(chara.Health <= 0){
            chara.IsAlive = false;
            controlSonido.PlayOneShot(deathSound);
            Destroy(gameObject, enemyData.WaitTime);
        }
    }
}