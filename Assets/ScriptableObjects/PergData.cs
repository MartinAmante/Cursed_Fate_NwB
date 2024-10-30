using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PowerUpType
{
    Armor,
    Speed,
    AttackSpeed,
    FireSword,
    
    
}
[CreateAssetMenu(fileName = "ParchmentsData", menuName = "Parchments Data")]
public class ParchmentsData : ScriptableObject
{
     [SerializeField] private int health;
     [SerializeField] private int maxHealth;
    // [SerializeField] private int lifes;
    //[SerializeField] private int maxLifes;
    //[SerializeField] private int weapon;
    //[SerializeField] private List<WeaponData> weaponList = new List<WeaponData>();
        [SerializeField] private PowerUpType newPowerUpType;        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float duration;
        [SerializeField] private float normalSpeed;
        //[SerializeField] private float runSpeed;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float cooldownSpeed;
        [SerializeField] private int attackDamage;
    //  [SerializeField] private float dashSpeed;
    //  [SerializeField] private float dashLength;
    // [SerializeField] private float dashCooldown;
    // [SerializeField] private bool isAlive;
    // [SerializeField] private bool isOnFire;
    // [SerializeField] private bool isWaiting;
    // [SerializeField] private bool isWalking;
    // [SerializeField] private bool isTurning;
    // [SerializeField] private bool isDashing;
    // [SerializeField] private bool isDashOnCooldown;
    // [SerializeField] private bool isAttacking;
    // [SerializeField] private bool isAttackOnCooldown;
    // [SerializeField] private bool isProtecting;

    void OnEnable()
        {
            moveSpeed = normalSpeed;
            health = maxHealth;
           // lifes = maxLifes;
          //  isAlive = true;
           // isOnFire = false;
           // isWaiting = true;
           // isWalking = false;
           // isTurning = false;
           // isDashing = false;
           // isAttacking = false;
           // isProtecting = false;
        }

        public int Health { get { return health; } set { health = value; } }
        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
       // public int Lifes { get { return lifes; } set { lifes = value; } }
       // public int MaxLifes { get { return maxLifes; } set { maxLifes = value; } }
       // public int Weapon { get { return weapon; } set { weapon = value; } }
       // public List<WeaponData> WeaponList { get { return weaponList; } set { weaponList = value; } }
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float NormalSpeed { get { return normalSpeed; } set { normalSpeed = value; } }
       // public float RunSpeed { get { return runSpeed; } set { runSpeed = value; } }
       // public float DashSpeed { get { return dashSpeed; } set { dashSpeed = value; } }
      //  public float DashLength { get { return dashLength; } set { dashLength = value; } }
      //  public float DashCooldown { get { return dashCooldown; } set { dashCooldown = value; } }
        public float Duration { get { return duration; } set { duration = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float CooldownSpeed { get { return cooldownSpeed; } set { cooldownSpeed = value; } }
        public PowerUpType NewPowerUpType { get {  return newPowerUpType; } set {  newPowerUpType = value; } }
        public int AttackDamage { get { return attackDamage; } set { attackDamage = value; } }


    //  public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    // public bool IsOnFire { get { return isOnFire; } set { isOnFire = value; } }
    // public bool IsWaiting { get { return isWaiting; } set { isWaiting = value; } }
    // public bool IsWalking { get { return isWalking; } set { isWalking = value; } }
    //  public bool IsTurning { get { return isTurning; } set { isTurning = value; } }
    //  public bool IsDashing { get { return isDashing; } set { isDashing = value; } }
    //  public bool IsDashOnCooldown { get { return isDashOnCooldown; } set { isDashOnCooldown = value; } }
    //   public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    //   public bool IsAttackOnCooldown { get { return isAttackOnCooldown; } set { isAttackOnCooldown = value; } }
    // public bool IsProtecting { get { return isProtecting; } set { isProtecting = value; } }
}

