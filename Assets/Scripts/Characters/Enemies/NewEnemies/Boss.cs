using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public GameObject hitboxAttack2;
    public GameObject hitbox;
    [SerializeField] private CharacterData enemy;
    [SerializeField] private EnemyData enemyData;
    public Transform playerTransform;
    public LayerMask Player;
    private bool itSpawning;
    private bool isAttackOne;
    private bool isAttackTwo;
    private bool isCooldownAttack = false;
    private bool isCooldownAttackTwo = false;
    private bool isTakingDamage = false;
    public GameObject circlePrefab;
    public Transform spawner;
    public float retreatSpeed = 10f;
    private enum EnemyState {NotSpawning, Spawning, Follow, AttackOne, AttackTwo }
    private EnemyState currentState = EnemyState.NotSpawning;


    void Start()
    {
        enemy.IsSpawning = false;
        enemy.IsWalking = false;
        enemy.IsWaiting = false;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        isAttackOne = Physics2D.OverlapCircle(transform.position, enemyData.DetectionRange, Player);
        isAttackTwo = Physics2D.OverlapCircle(transform.position, enemyData.DetectionAttack, Player);
        itSpawning = Physics2D.OverlapCircle(transform.position, enemyData.DetectionSpawn, Player);
        if (isTakingDamage)
        {
            return; 
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        UpdateState(distanceToPlayer);
        ExecuteState();      

    }
    void UpdateState(float distanceToPlayer)
    {
        if (distanceToPlayer <= enemyData.DetectionSpawn)
        {
            currentState = EnemyState.Spawning;
        }       
    }

    void ExecuteState()
    {
        switch (currentState)
        {
            case EnemyState.Spawning:

                enemy.IsSpawning = true;                
                break;
            case EnemyState.Follow:
                enemy.IsAttacking = false;
                enemy.IsWalking = true;
                Follow();
                Flip();
                break;

            case EnemyState.AttackOne:
                if (!isCooldownAttack)
                {
                    hitboxAttack2.SetActive(false);
                    isCooldownAttackTwo = false;
                    enemy.IsWalking = false;
                    StartCoroutine(AttackOne());
                }
                break;

            case EnemyState.AttackTwo:
                if (!isCooldownAttackTwo)
                {
                    isCooldownAttack = false;
                    isCooldownAttackTwo = false;
                    hitboxAttack2.SetActive(false);
                    enemy.IsAttacking = false;
                    //enemy.IsWalking = false;
                    StartCoroutine(AttackTwo());
                }
                break;
        }
    }

    void Follow()
    {
        if (playerTransform != null & enemy.IsWalking)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemy.NormalSpeed * Time.deltaTime);
        }
    }

    IEnumerator AttackOne()
    {
        if (enemy.IsAttacking) yield break;

        isCooldownAttack = true;
        enemy.IsAttacking = true;
        yield return new WaitForSeconds(enemyData.IsCooldownMid);

        GameObject circle = Instantiate(circlePrefab, spawner.position, Quaternion.identity);
        Rigidbody2D rb = circle.GetComponent<Rigidbody2D>();
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        rb.velocity = direction * enemy.RunSpeed;
        Destroy(circle, 3f);

        enemy.IsAttacking = false;
        yield return new WaitForSeconds(enemyData.IsCooldown);
        isCooldownAttack = false;
    }

    IEnumerator AttackTwo()
    {
        isCooldownAttackTwo = true;
        enemy.IsAttackingTwo = true;

        hitboxAttack2.SetActive(true);
        yield return new WaitForSeconds(enemyData.IsCooldownAnimTwo);
        hitboxAttack2.SetActive(false);

        Vector2 direction = (transform.position - playerTransform.position).normalized;
        transform.position += (Vector3)direction * retreatSpeed * Time.deltaTime;

        enemy.IsAttackingTwo = false;
        enemy.IsWalking = true;
        isCooldownAttackTwo = false;
    }

    void Hurt()
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true;
            StopAllCoroutines();
            enemy.IsAttacking = false;
            enemy.IsWalking = false;
            enemy.IsHurt = true;
            StartCoroutine(RecoverFromDamage());
        }
    }

    IEnumerator RecoverFromDamage()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.IsHurt = false;
        isTakingDamage = false;
    }

    void Flip()
    {

        if (playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 1);
        }
        else
        {
            transform.localScale = new Vector3(5, 5, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionAttack);
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionSpawn);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerWeaponHitbox"))
        {
            Hurt();
        }
    }
}
