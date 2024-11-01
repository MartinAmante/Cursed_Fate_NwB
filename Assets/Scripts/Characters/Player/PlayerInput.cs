using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour{
    [SerializeField]public CharacterData player;

    public delegate void DirectionalInput(float xAxis, float yAxis);
    public static DirectionalInput direction;

    public delegate void DashInput();
    public static DashInput dash;

    public delegate void AttackInput();
    public static AttackInput attack;

    public delegate void ProtectInput();
    public static ProtectInput protect;

    public delegate void ProtectReleaseInput();
    public static ProtectReleaseInput protectRelease;

    public delegate void InteractInput();
    public static InteractInput interact;
    private float xAxis, yAxis;
    private bool directionPressed, directionReleased, dashPressed, attackPressed, protectPressed, protectReleased;

    void Update(){
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        if(player.IsAlive){
            directionPressed = xAxis != 0 || yAxis != 0;
            dashPressed = Input.GetButton("Dash");
            attackPressed = Input.GetButton("Attack");
            protectPressed = Input.GetButton("Protect");
        }else{
            directionPressed = false;
            dashPressed = false;
            attackPressed = false;
            protectPressed = false;
        }

        if(directionPressed) {direction(xAxis, yAxis); directionReleased = false;}
        if(!directionPressed && !directionReleased) {direction(0, 0); directionReleased = true;}
        if(dashPressed) dash();
        if(attackPressed) attack();
        if(protectPressed) {protect(); protectReleased = false;}
        if(!protectPressed && !protectReleased) {protectRelease();  protectReleased = true;}
        //if(Input.GetButton("Interact")) interact();
    }
}