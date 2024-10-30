using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ChestAnimation : MonoBehaviour
{
    [SerializeField] private ChestsData chest;
    [SerializeField] private Animator anim;
    

    // Update is called once per frame
    void Update()
    {
        Open();
    }
    public void Open()
    {
        anim.SetBool("IsOpening", chest.IsOpening);

    }
}
