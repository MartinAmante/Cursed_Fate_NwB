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
    //private bool isAttackTwo;
    public GameObject circlePrefab;    
    public Transform spawner;
    public bool isTakingDamage = false;


    void Start()
    {
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
        // isAttackTwo = Physics2D.OverlapCircle(transform.position, enemyData.DetectionAttack, Player);
        if (isTakingDamage)
        {
            return; // Detiene cualquier otra lógica en el Update
        }

        if (!isAttackOne)
        {
            enemyData.IsCooldownAttack = false;
            enemy.IsAttacking = false;
            enemy.IsWalking = true;
            Follow();
        }
        else 
        {
            enemy.IsWalking = false;           
            if (!enemyData.IsCooldownAttack)
            {
                enemy.IsAttacking = true;
                StartCoroutine(AttackOne());
            }  
        
        }
        Flip();


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
        enemyData.IsCooldownAttack = true;
        yield return new WaitForSeconds(enemyData.IsCooldownMid);
        GameObject circle = Instantiate(circlePrefab, spawner.position, Quaternion.identity);
        Rigidbody2D rb = circle.GetComponent<Rigidbody2D>();
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        rb.velocity = direction * enemy.RunSpeed;
        Destroy(circle, 3f);
        enemy.IsAttacking = false;
        yield return new WaitForSeconds(enemyData.IsCooldown);
        enemyData.IsCooldownAttack = false;
    }

    IEnumerator AttackTwo()
    {
        enemyData.IsCooldownAttackTwo = true;
        hitboxAttack2.SetActive(true);
        yield return new WaitForSeconds(enemyData.IsCooldown);
        hitboxAttack2.SetActive(false);
        enemyData.IsCooldownAttackTwo = false;
    }

    void Hurt()
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true; // Cambia el estado a "recibiendo daño"
            StopAllCoroutines(); // Detiene todas las corrutinas, como los ataques
            enemy.IsAttacking = false;
            enemy.IsWalking = false;

            // Reproduce la animación de recibir daño
            enemy.IsHurt = true;

            // Espera un breve periodo antes de permitir que el enemigo vuelva a la acción
            StartCoroutine(RecoverFromDamage());
        }
    }

    IEnumerator RecoverFromDamage()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.IsHurt = false;
        isTakingDamage = false; // Permite que el enemigo vuelva a sus acciones normales
    }

    void Flip()
    {       

        if (playerTransform.position.x < transform.position.x)
        {
            // Voltear hacia la izquierda
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else
        {
            // Voltear hacia la derecha
            transform.localScale = new Vector3(2, 2, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
       // Gizmos.DrawWireSphere(transform.position, enemyData.DetectionAttack);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el enemigo toca al jugador
        if (other.CompareTag("PlayerWeaponHitbox"))
        {
            Hurt();
        }
    }
}
