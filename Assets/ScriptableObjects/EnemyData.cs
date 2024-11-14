using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject{
    [SerializeField] private int weapon;
    [SerializeField] private List<WeaponData> weaponList = new List<WeaponData>();
    [SerializeField] private float detectionRange;
    [SerializeField] private float reactionTime;
    [SerializeField] private float waitTime;
    [SerializeField] private float detectionAttack;
    [SerializeField] private int isCooldown;
    [SerializeField] private float isCooldownMid;
    [SerializeField] private bool isAlert;
    [SerializeField] private bool isSpawning;
    [SerializeField] private bool isCooldownAttack;
    [SerializeField] private bool isCooldownAttackTwo;

    public int Weapon { get { return weapon; } set { weapon = value; } }
    public List<WeaponData> WeaponList { get { return weaponList; } set { weaponList = value; } }
    public float DetectionRange {get {return detectionRange;} set {detectionRange = value;}}
    public float ReactionTime {get {return reactionTime;} set {reactionTime = value;}}
    public float WaitTime {get {return waitTime;} set {waitTime = value;}}
    public bool IsAlert {get {return isAlert;} set {isAlert = value;}}
    public bool IsSpawning { get { return isSpawning; } set { isSpawning = value; } }
    public bool IsCooldownAttack { get { return isCooldownAttack; } set { isCooldownAttack = value; } }
    public int IsCooldown { get { return isCooldown; } set { isCooldown = value; } }
    public float DetectionAttack { get { return detectionAttack; } set { detectionAttack = value; } }
    public float IsCooldownMid { get { return isCooldownMid; } set { isCooldownMid = value; } }
    public bool IsCooldownAttackTwo { get { return isCooldownAttackTwo; } set { isCooldownAttackTwo = value; } }}