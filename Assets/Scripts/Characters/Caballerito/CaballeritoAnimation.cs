using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeritoAnimation : MonoBehaviour
{
    [SerializeField] private CharacterData caballeritoDataAnim;
    [SerializeField] private Animator caballeritoAnimator;

    void Update()
    {
        Movement();
        Attack();
    }

    public void Attack()
    {
        caballeritoAnimator.SetInteger("idWeapon", caballeritoDataAnim.Weapon);
        caballeritoAnimator.SetBool("isAttacking", caballeritoDataAnim.IsAttacking);
        caballeritoAnimator.SetBool("isAttackOnCooldown", caballeritoDataAnim.IsAttackOnCooldown);
        caballeritoAnimator.SetBool("isOnFire", caballeritoDataAnim.IsOnFire);
    }

    public void Movement()
    {
        caballeritoAnimator.SetBool("isWaiting", caballeritoDataAnim.IsWaiting);
        caballeritoAnimator.SetBool("isWalking", caballeritoDataAnim.IsWalking);
        caballeritoAnimator.SetBool("isDashing", caballeritoDataAnim.IsDashing);
        caballeritoAnimator.SetBool("isDashOnCooldown", caballeritoDataAnim.IsDashOnCooldown);
        caballeritoAnimator.SetBool("isAlive", caballeritoDataAnim.IsAlive);
    }
}
