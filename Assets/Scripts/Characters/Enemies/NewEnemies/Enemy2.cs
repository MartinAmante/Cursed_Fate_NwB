using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject player;
    public GameObject hitbox;
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyData enemyData;
    public Transform playerTransform;
    public LayerMask Player;
    private bool isAttackOne;
    private bool isCooldownAttack = false;
    private bool isTakingDamage = false;
    public float retreatSpeed = 10f;
    private enum EnemyState { Follow, AttackOne, AttackTwo }
    private EnemyState currentState = EnemyState.Follow;


    void Start()
    {
        enemy.chara.IsWalking = false;
        enemy.chara.IsWaiting = false;
        enemy.chara.IsHurt = false;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        isAttackOne = Physics2D.OverlapCircle(transform.position, enemyData.DetectionRange, Player);       
        if (isTakingDamage)
        {
            return; // Detiene cualquier otra lógica en el Update
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        UpdateState(distanceToPlayer);
        ExecuteState();
        Flip();

    }
    void UpdateState(float distanceToPlayer)
    {
        if (distanceToPlayer <= enemyData.DetectionRange) // Ataque lejano
        {
            currentState = EnemyState.AttackOne;
        }
        else // Fuera de rango de ataque
        {
            currentState = EnemyState.Follow;
        }
    }

    void ExecuteState()
    {
        switch (currentState)
        {
            case EnemyState.Follow:

                
                isCooldownAttack = false;
                enemy.chara.IsAttacking = false;
                enemy.chara.IsWalking = true;
                Follow();
                break;

            case EnemyState.AttackOne:
                if (!isCooldownAttack)
                {
                    enemy.chara.IsWalking = false;
                    StartCoroutine(AttackOne());
                }
                break;
        }
    }

    void Follow()
    {
        if (playerTransform != null & enemy.chara.IsWalking)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemy.chara.RunSpeed * Time.deltaTime);
        }
    }

    IEnumerator AttackOne()
    {
        if (enemy.chara.IsAttacking) yield break;

        isCooldownAttack = true;
        enemy.chara.IsAttacking = true;     
        yield return new WaitForSeconds(enemyData.IsCooldownMid);
        hitbox.SetActive(true);
        yield return new WaitForSeconds(enemyData.IsCooldown);
        hitbox.SetActive(false);
        enemy.chara.IsAttacking = false;       
        yield return new WaitForSeconds(enemyData.IsCooldown);
        isCooldownAttack = false;
    }

    void Hurt()
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true;
            StopAllCoroutines();
            enemy.chara.IsAttacking = false;
            enemy.chara.IsWalking = false;
            enemy.chara.IsHurt = true;
            StartCoroutine(RecoverFromDamage());
        }
    }

    IEnumerator RecoverFromDamage()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.chara.IsHurt = false;
        isTakingDamage = false;
    }

    void Flip()
    {

        if (playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerWeaponHitbox"))
        {
            Hurt();
        }
    }
}
