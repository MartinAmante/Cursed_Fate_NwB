using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour{
    [SerializeField]private CharacterData chara;
    [SerializeField]private Animator anim;

    void Update(){
        Movement();
        Attack();
    }

    public void Attack(){
        anim.SetInteger("idWeapon", chara.Weapon);
        anim.SetBool("isAttacking", chara.IsAttacking);
        anim.SetBool("isAttackOnCooldown", chara.IsAttackOnCooldown);
        anim.SetBool("isOnFire", chara.IsOnFire);
    }

    public void Movement(){
        anim.SetBool("isWaiting", chara.IsWaiting);
        anim.SetBool("isWalking", chara.IsWalking);
        anim.SetBool("isDashing", chara.IsDashing);
        anim.SetBool("isDashOnCooldown", chara.IsDashOnCooldown);
    }
}