using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CaballeritoBehaviour;

public class Enemy : Character{

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private int enemyID;
    public override void CheckHealth(){
        if(chara.Health <= 0){
            chara.IsAlive = false;
            Destroy(gameObject, enemyData.WaitTime);
        }
        

    }
      void Start()
    {
        switch (enemyID)
        {
            case 1:
                chara.MaxHealth = 50;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes; 
                chara.MaxLifes = 1;
                chara.WeaponList = new List<WeaponData> { weaponData };
                chara.RunSpeed = 4;
                chara.IsAttacking = false;
                break;
            case 2:
                chara.MaxHealth = 50;
                chara.Health = chara.MaxHealth;
                chara.MaxLifes = 1;
                chara.WeaponList = new List<WeaponData> { weaponData };
                chara.RunSpeed = 4;
                chara.IsAttacking = false;
                break;
            case 3:
                chara.MaxHealth = 50;
                chara.Health = chara.MaxHealth;
                chara.MaxLifes = 1;
                chara.WeaponList = new List<WeaponData> { weaponData };
                chara.RunSpeed = 4;
                chara.IsAttacking = false;
                break;
        }

    }

}