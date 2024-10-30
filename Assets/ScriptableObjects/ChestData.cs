using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChestsData", menuName = "Chests Data")]

public class ChestsData : ScriptableObject
{
    [SerializeField] private bool isOpening;
    [SerializeField] private float cooldownAnimation;
    [SerializeField] private float cooldownInteract;
    [SerializeField] private List<GameObject> parchmentList;
    [SerializeField] private bool islooted;


    void OnEnable()
    {
        isOpening = false;
        Islooted = false;
    }

    public bool IsOpening { get { return isOpening; } set { isOpening = value; } }
    public bool Islooted { get { return islooted; } set { islooted = value; } }
    public float CooldownInteract { get {return cooldownInteract; } set { cooldownInteract = value; } }
    public float CooldownAnimation{ get { return cooldownAnimation; } set { cooldownAnimation = value; } }

    public List<GameObject> ParchmentList { get { return parchmentList; } set { parchmentList = value; } }
}

