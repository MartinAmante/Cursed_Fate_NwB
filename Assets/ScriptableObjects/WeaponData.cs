using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject{
    [SerializeField]private int attackDamage;
    [SerializeField]private float attackSpeed;
    [SerializeField]private float attackDuration;
    [SerializeField]private float attackCooldown;
    [SerializeField]private bool isOnFire;
    [SerializeField]private GameObject hitbox;
    [SerializeField]private float hitboxStart;
    [SerializeField]private float hitboxFinish;

    public int AttackDamage {get {return attackDamage;} set {attackDamage = value;}}
    public float AttackSpeed {get {return attackSpeed;} set {attackSpeed = value;}}
    public float AttackDuration {get {return attackDuration;} set {attackDuration = value;}}
    public float AttackCooldown {get {return attackCooldown;} set {attackCooldown = value;}}
    public bool IsOnFire {get {return isOnFire;} set {isOnFire = value;}}
    public GameObject Hitbox {get {return hitbox;} set {hitbox = value;}}
    public float HitboxStart {get {return hitboxStart;} set {hitboxStart = value;}}
    public float HitboxFinish {get {return hitboxFinish;} set {hitboxFinish = value;}}
}