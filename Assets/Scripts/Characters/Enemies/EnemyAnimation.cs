using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour{
    [SerializeField]private CharacterData enemy;
    [SerializeField] private EnemyData enemyData;
    [SerializeField]private SpriteRenderer sprite;
    [SerializeField]private Animator anim;

    void Update(){
        //sprite.flipX = enemy.IsTurning;
        Movement();
        Attack();
    }
    public void Attack()
    {
        anim.SetBool("isAttacking", enemy.IsAttacking);
        anim.SetBool("isAttackOnCooldown", enemy.IsAttackOnCooldown);
        anim.SetBool("isOnFire", enemy.IsOnFire);
        anim.SetInteger("idWeapon", enemy.Weapon );
    }
    public void Movement()
    {
        anim.SetBool("isAlive", enemy.IsAlive);
        anim.SetBool("isSpawning", enemyData.IsSpawning);
        anim.SetBool("isWaiting", enemy.IsWaiting);
        anim.SetBool("isWalking", enemy.IsWalking);
        anim.SetBool("isDashing", enemy.IsDashing);
        anim.SetBool("isDashOnCooldown", enemy.IsDashOnCooldown);
    }
}