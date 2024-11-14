using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject player;
    public GameObject hitboxAttack2;
    public GameObject hitbox;
    [SerializeField] private CharacterData enemy;
    [SerializeField] private EnemyData enemyData;
    public Transform playerTransform;
    public LayerMask Player;
    private bool isAttackOne;
    private bool isAttackTwo;
    private bool isCooldownAttack = false;
    private bool isCooldownAttackTwo = false;
    private bool isTakingDamage = false;
    public GameObject circlePrefab;    
    public Transform spawner;
    public float retreatSpeed = 10f;
    private enum EnemyState { Follow, AttackOne, AttackTwo }
    private EnemyState currentState = EnemyState.Follow;


    void Start()
    {
        enemy.IsWalking = false;
        enemy.IsWaiting = false;
        enemy.IsHurt = false;
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
        if (distanceToPlayer <= enemyData.DetectionAttack) // Ataque cercano
        {
            currentState = EnemyState.AttackTwo;
        }
        else if (distanceToPlayer <= enemyData.DetectionRange) // Ataque lejano
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

                hitboxAttack2.SetActive(false);
                isCooldownAttack = false;
                isCooldownAttackTwo = false;
                enemy.IsAttacking = false;
                enemy.IsWalking = true;
                Follow();
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
        isTakingDamage= false; 
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
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionAttack);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerWeaponHitbox"))
        {
            Hurt();
        }
    }
}
