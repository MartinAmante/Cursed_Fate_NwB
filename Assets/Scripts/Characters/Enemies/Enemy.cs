using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character{

    [SerializeField] private EnemyData enemyData;
    public override void CheckHealth(){
        if(chara.Health <= 0){
            chara.IsAlive = false;
            Destroy(gameObject, enemyData.WaitTime);
        }
    }
}