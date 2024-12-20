using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    [SerializeField]private CharacterData player;
    [SerializeField]private Rigidbody2D rb2d;
    [SerializeField]private Vector2 moveInput;
    [SerializeField] public GameObject playerFinal;
    void Start(){
        rb2d = playerFinal.GetComponent<Rigidbody2D>();
        PlayerInput.direction += Move;
    }
    void Update() 
    { 
        //playerFinal = GameObject.FindGameObjectWithTag("Player"); rb2d = playerFinal.GetComponent<Rigidbody2D>();
    }

    void Move(float xAxis, float yAxis){
        moveInput.x = xAxis;
        moveInput.y = yAxis;
        moveInput.Normalize();
        rb2d.velocity = moveInput * player.MoveSpeed;
        if(rb2d.velocity.x != 0 || rb2d.velocity.y != 0){
            player.IsWalking = true;
        }else{
            player.IsWalking = false;
        }
        if(xAxis < 0){
            player.IsTurning = true;
        }
        if(xAxis > 0){
            player.IsTurning = false;
        }
    }
}