using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconActivate : MonoBehaviour
{
    [SerializeField] Animator letterAnimator;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            letterAnimator.SetBool("activate", true); 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            letterAnimator.SetBool("activate", false);
        }
    }
}
