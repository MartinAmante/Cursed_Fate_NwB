using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject{
    [SerializeField]private int health;
    [SerializeField]private int maxHealth;
    [SerializeField]private int lifes;
    [SerializeField]private int maxLifes;
    [SerializeField]private int weapon;
    [SerializeField]private List<WeaponData> weaponList = new List<WeaponData>();
    [SerializeField]private float moveSpeed;
    [SerializeField]private float normalSpeed;
    [SerializeField]private float runSpeed;
    [SerializeField]private float dashSpeed;
    [SerializeField]private float dashLength;
    [SerializeField]private float dashCooldown;
    [SerializeField]private bool isAlive;
    [SerializeField]private bool isOnFire;
    [SerializeField]private bool isWaiting;
    [SerializeField]private bool isWalking;
    [SerializeField]private bool isTurning;
    [SerializeField]private bool isDashing;
    [SerializeField]private bool isDashOnCooldown;
    [SerializeField]private bool isAttacking;
    [SerializeField]private bool isAttackOnCooldown;
    [SerializeField]private bool isProtecting;

    void OnEnable(){
        moveSpeed = normalSpeed;
        health = maxHealth;
        lifes = maxLifes;
        isAlive = true;
        isOnFire = false;
        isWaiting = true;
        isWalking = false;
        isTurning = false;
        isDashing = false;
        isAttacking = false;
        isProtecting = false;
    }

    public int Health {get {return health;} set {health = value;}}
    public int MaxHealth {get {return maxHealth;} set {maxHealth = value;}}
    public int Lifes {get {return lifes;} set {lifes = value;}}
    public int MaxLifes {get {return maxLifes;} set {maxLifes = value;}}
    public int Weapon {get {return weapon;} set {weapon = value;}}
    public List<WeaponData> WeaponList {get {return weaponList;} set {weaponList = value;}}
    public float MoveSpeed {get {return moveSpeed;} set {moveSpeed = value;}}
    public float NormalSpeed {get {return normalSpeed;} set {normalSpeed = value;}}
    public float RunSpeed { get { return runSpeed; } set { runSpeed = value; } }
    public float DashSpeed {get {return dashSpeed;} set {dashSpeed = value;}}
    public float DashLength {get {return dashLength;} set {dashLength = value;}}
    public float DashCooldown {get {return dashCooldown;} set {dashCooldown = value;}}
    public bool IsAlive {get {return isAlive;} set {isAlive = value;}}
    public bool IsOnFire {get {return isOnFire;} set {isOnFire = value;}}
    public bool IsWaiting {get {return isWaiting;} set {isWaiting = value;}}
    public bool IsWalking {get {return isWalking;} set {isWalking = value;}}
    public bool IsTurning {get {return isTurning;} set {isTurning = value;}}
    public bool IsDashing {get {return isDashing;} set {isDashing = value;}}
    public bool IsDashOnCooldown { get { return isDashOnCooldown; } set { isDashOnCooldown = value; } }
    public bool IsAttacking {get {return isAttacking;} set {isAttacking = value;}}
    public bool IsAttackOnCooldown { get { return isAttackOnCooldown; } set { isAttackOnCooldown = value; } }
    public bool IsProtecting {get {return isProtecting;} set {isProtecting = value;}}
}