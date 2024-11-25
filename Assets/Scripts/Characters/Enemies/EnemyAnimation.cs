using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour{
    [SerializeField]private Enemy enemy;
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
        anim.SetBool("isAttacking", enemy.chara.IsAttacking);
        anim.SetBool("isAttackingTwo", enemy.chara.IsAttackingTwo);
        anim.SetBool("isHurt", enemy.chara.IsHurt);
        anim.SetBool("isAttackOnCooldown", enemy.chara.IsAttackOnCooldown);
        anim.SetBool("isOnFire", enemy.chara.IsOnFire);
        anim.SetInteger("idWeapon", enemy.chara.Weapon);
    }
    public void Movement()
    {
        anim.SetBool("isAlive", enemy.chara.IsAlive);
        anim.SetBool("isSpawning", enemy.chara.IsSpawning);
        anim.SetBool("isWaiting", enemy.chara.IsWaiting);
        anim.SetBool("isWalking", enemy.chara.IsWalking);
        anim.SetBool("isDashing", enemy.chara.IsDashing);
        anim.SetBool("isDashOnCooldown", enemy.chara.IsDashOnCooldown);
    }
}