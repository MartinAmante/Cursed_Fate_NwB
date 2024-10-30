using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class golemBehaviour : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    public GameObject PointC;
    public GameObject PointD;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = PointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb .velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointB.transform)
        {
            currentPoint = PointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA.transform)
        {
            currentPoint = PointB.transform;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position,  0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        
    }
}
