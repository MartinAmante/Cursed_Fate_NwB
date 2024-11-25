using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CaballeritoBehaviour;

public class Enemy : Character{

    [SerializeField] private EnemyData enemyData;
    //[SerializeField] private WeaponData weaponData;
    [SerializeField] private List<WeaponData> weaponList = new();
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
            /*          Sprout                  */
            case 1:
                chara.MaxHealth = 50;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes;
                chara.MaxLifes = 1;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 4;
                chara.IsAttacking = false;
                break;
            /*          Skeleton                 */
            case 2:
                chara.MaxHealth = 100;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes;
                chara.MaxLifes = 1;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 4;
                chara.IsAttacking = false;
                break;
            /*           Seeker                  */
            case 3:
                chara.MaxHealth = 100;
                chara.Health = chara.MaxHealth;
                chara.MaxLifes = 1;
                chara.Lifes = chara.MaxLifes;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 4;
                chara.IsAttacking = false;
                break;
            /*          Old Guardian              */
            case 4:
                chara.MaxHealth = 100;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes;
                chara.MaxLifes = 1;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 3;
                chara.IsAttacking = false;
                break;
            /*           Golem                 */
            case 5:
                chara.MaxHealth = 100;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes;
                chara.MaxLifes = 1;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 2;
                chara.IsAttacking = false;
                break;
            /*          Demon                  */
            case 6:
                chara.MaxHealth = 200;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes;
                chara.MaxLifes = 1;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 1;
                chara.IsAttacking = false;
                break;
            /*          New Enemy 1            */
            case 7:
                chara.MaxHealth = 100;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes;
                chara.MaxLifes = 1;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 2;
                chara.IsAttacking = false;
                break;
            /*         New Enemy 2             */
            case 8:
                chara.MaxHealth = 100;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes;
                chara.MaxLifes = 1;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 3;
                chara.IsAttacking = false;
                break;
            /*          Boss                  */
            case 9:
                chara.MaxHealth = 100;
                chara.Health = chara.MaxHealth;
                chara.Lifes = chara.MaxLifes;
                chara.MaxLifes = 1;
                chara.WeaponList = weaponList;
                chara.RunSpeed = 3;
                chara.IsAttacking = false;
                break;
        }

    }

}