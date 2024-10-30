using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour{
    [SerializeField]private CharacterData player;

    void Start(){
        PlayerInput.dash += Dash;
    }

    void Dash(){
        if(!player.IsDashing && !player.IsDashOnCooldown){
            player.IsDashing = true;
            StartCoroutine(Dash1());
        }
    }

    IEnumerator Dash1(){
        player.MoveSpeed = player.DashSpeed;
        yield return new WaitForSeconds(player.DashLength);
        player.MoveSpeed = player.NormalSpeed;
        player.IsDashing = false;
        player.IsDashOnCooldown = true;
        yield return new WaitForSeconds(player.DashCooldown);
        player.IsDashOnCooldown = false;
    }
}