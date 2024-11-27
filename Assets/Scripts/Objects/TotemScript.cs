using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
   public bool isActive = false;
    public Animator anim;
    public GameObject caballeritoObject;
    public Transform spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            anim.SetBool("isActivate", true);
            GameObject caballerito = Instantiate(caballeritoObject, spawner.position, Quaternion.identity);
            isActive = true;
        }
    }
}
