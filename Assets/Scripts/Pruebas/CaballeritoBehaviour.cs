using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeritoBehaviour : MonoBehaviour
{
    [SerializeField] public Transform playerPosition;
    [SerializeField] public Transform enemyPosition;
    [SerializeField] public float stopPosition;
    [SerializeField] private int moveSpeed;
    [SerializeField] public float detectionRange;
    [SerializeField] public float maxDistance;
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] public EnemyState actualState;
    [SerializeField] public enum EnemyState
    {
        Waiting,
        Attacking,
        Returning,
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        attackProgress = 0f;
        if (playerPosition == null)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (actualState)
        {
            case EnemyState.Waiting:
                WaitingState();
                break;
            case EnemyState.Attacking:
                AttackingState();
                break;
            case EnemyState.Returning:
                ReturningState();
                break;
        }
    }

    private void WaitingState()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, enemyLayer);
        if (playerCollider)
        {
                enemyPosition = playerCollider.transform;
                actualState = EnemyState.Attacking;
        }
        else if (!playerCollider)
        {
            if (Vector2.Distance(transform.position, playerPosition.position) > stopPosition)
            {
                Vector2 tVe2;
                tVe2 = Vector2.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
                this.transform.position = tVe2;
            }
        }
    }
    
    private void AttackingState()
    {
        if (enemyPosition == null)
        {
            actualState = EnemyState.Returning;
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, enemyPosition.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, playerPosition.position) > maxDistance || Vector2.Distance(transform.position, enemyPosition.position) > maxDistance)
        {
            actualState = EnemyState.Returning;
            enemyPosition = null;
        }
        if (Vector2.Distance(transform.position, enemyPosition.position) < 2f)
        {
            Attack();
        }
    }
    private void ReturningState()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, playerPosition.position) < 0.5f)
        {
            actualState = EnemyState.Waiting;
        }
    }
    /*                                 Ataque del caballerito                                                            */
    [SerializeField] private WeaponData weapon;
    [SerializeField] private Transform hitboxField;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isAttackOnCooldown;
    [SerializeField] private float start;
    [SerializeField] private float finish;
    [SerializeField] private float attackProgress;
    private void Attack()
    {
        if (!isAttacking && !isAttackOnCooldown)
        {
            isAttacking = true;
            StartCoroutine(Attack1());
        }
    }
    IEnumerator Attack1()
    {
        hitbox = Instantiate(weapon.Hitbox, hitboxField);
        start = weapon.HitboxStart;
        finish = weapon.HitboxFinish;
        while (attackProgress < 1)
        {
            attackProgress += weapon.AttackSpeed * Time.deltaTime;
            hitbox.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(start, finish, attackProgress));
            yield return null;
        }
        yield return new WaitForSeconds(weapon.AttackDuration);
        Destroy(hitbox);
        isAttacking = false;
        isAttackOnCooldown = true;
        yield return new WaitForSeconds(weapon.AttackCooldown);
        attackProgress = 0f;
        isAttackOnCooldown = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(playerPosition.position, maxDistance);
    }
}
