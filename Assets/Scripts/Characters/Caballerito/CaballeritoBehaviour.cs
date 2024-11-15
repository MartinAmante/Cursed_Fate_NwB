using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaballeritoBehaviour : MonoBehaviour
{
    [SerializeField] public Transform playerPosition;
    [SerializeField] public Transform enemyPosition;
    [SerializeField] public Vector3 attackPosition;
    [SerializeField] public float stopPosition;
    [SerializeField] public float detectionRange;
    [SerializeField] public float maxDistance;
    [SerializeField] public float distance;
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] public EnemyState actualState;
    [SerializeField] public SpriteRenderer spriteRenderer;
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
        if (enemyPosition != null)
        {
            distance = Vector2.Distance(transform.position, enemyPosition.position);
        } 
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
        caballeritoData.IsWaiting = true;
        caballeritoData.IsAttacking = false;
        caballeritoData.IsWalking = false;
        if (playerCollider)
        {
                enemyPosition = playerCollider.transform;
                actualState = EnemyState.Attacking;
        }
        else if (!playerCollider)
        {
            if (Vector2.Distance(transform.position, playerPosition.position) > stopPosition)
            {
                if (transform.position.x < playerPosition.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    Vector2 tVe2;
                    caballeritoData.IsWalking = true;
                    caballeritoData.IsWaiting = false;
                    tVe2 = Vector2.MoveTowards(transform.position, playerPosition.position, caballeritoData.MoveSpeed * Time.deltaTime);
                    this.transform.position = tVe2;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    Vector2 tVe2;
                    caballeritoData.IsWalking = true;
                    caballeritoData.IsWaiting = false;
                    tVe2 = Vector2.MoveTowards(transform.position, playerPosition.position, caballeritoData.MoveSpeed * Time.deltaTime);
                    this.transform.position = tVe2;
                }    
                
            }
        }
    }
    
    private void AttackingState()
    {
        caballeritoData.IsWaiting = false;
        caballeritoData.IsWalking = false;
        if (enemyPosition == null)
        {
            actualState = EnemyState.Returning;
            return;
        }
        if(transform.position.x < enemyPosition.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            attackPosition = new Vector3( enemyPosition.position.x - 2f, enemyPosition.position.y, enemyPosition.position.z );
            transform.position = Vector2.MoveTowards(transform.position, attackPosition, caballeritoData.MoveSpeed * Time.deltaTime);
            caballeritoData.IsWalking = true;
            if (distance < 3f)
            {
                caballeritoData.IsWalking = false;
                Attack();
            }

        }
        else if (transform.position.x > enemyPosition.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            caballeritoData.IsWalking = true;
            attackPosition = new Vector3(enemyPosition.position.x + 2f, enemyPosition.position.y, enemyPosition.position.z);
            transform.position = Vector2.MoveTowards(transform.position, attackPosition, caballeritoData.MoveSpeed * Time.deltaTime);
            if (distance < 3f)
            {
                caballeritoData.IsWalking = false;
                Attack();
            }
        }
        
        if (Vector2.Distance(transform.position, playerPosition.position) > maxDistance || Vector2.Distance(transform.position, enemyPosition.position) > maxDistance)
        {
            actualState = EnemyState.Returning;
            //enemyPosition = null;
        }
        
    }
    private void ReturningState()
    {
        caballeritoData.IsWaiting = false;
        caballeritoData.IsAttacking = false;
        caballeritoData.IsWalking = true;
        if (transform.position.x < playerPosition.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Vector3 returnPosition = new Vector3(playerPosition.position.x -2f, playerPosition.position.y, playerPosition.position.z);
            transform.position = Vector2.MoveTowards(transform.position, returnPosition, caballeritoData.MoveSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Vector3 returnPosition = new Vector3(playerPosition.position.x + 2f, playerPosition.position.y, playerPosition.position.z);
            transform.position = Vector2.MoveTowards(transform.position, returnPosition, caballeritoData.MoveSpeed * Time.deltaTime);
        }
            
        if (Vector2.Distance(transform.position, playerPosition.position) < 3f)
        {
            actualState = EnemyState.Waiting;
            caballeritoData.IsWalking = false;
        }
    }
    /*                                 Ataque del caballerito                                                            */
    [SerializeField] private CharacterData caballeritoData;
    [SerializeField] private Transform hitboxField;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private float start;
    [SerializeField] private float finish;
    [SerializeField] private float attackProgress;
    private void Attack()
    {
        if (!caballeritoData.IsAttacking && !caballeritoData.IsAttackOnCooldown)
        {
            caballeritoData.IsAttacking = true;
            caballeritoData.IsWaiting = false;
            caballeritoData.IsWalking = false;
            StartCoroutine(Attack1());
        }
    }
    IEnumerator Attack1()
    {
        caballeritoData.IsWalking = false;
        hitbox = Instantiate(caballeritoData.WeaponList[caballeritoData.Weapon].Hitbox, hitboxField);
        start = caballeritoData.WeaponList[caballeritoData.Weapon].HitboxStart;
        finish = caballeritoData.WeaponList[caballeritoData.Weapon].HitboxFinish;
        while (attackProgress < 1)
        {
            attackProgress += caballeritoData.WeaponList[caballeritoData.Weapon].AttackSpeed * Time.deltaTime;
            hitbox.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(start, finish, attackProgress));
            yield return null;
        }
        yield return new WaitForSeconds(caballeritoData.WeaponList[caballeritoData.Weapon].AttackDuration);
        Destroy(hitbox);
        caballeritoData.IsAttacking = false;
        caballeritoData.IsAttackOnCooldown = true;
        yield return new WaitForSeconds(caballeritoData.WeaponList[caballeritoData.Weapon].AttackCooldown);
        attackProgress = 0f;
        caballeritoData.IsAttackOnCooldown = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(playerPosition.position, maxDistance);
    }
}
