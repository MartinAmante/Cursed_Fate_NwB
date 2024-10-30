using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProtect : MonoBehaviour{
    [SerializeField]private CharacterData player;
    [SerializeField]private GameObject shield;

    void Start(){
        PlayerInput.protect += EnableShield;
        PlayerInput.protectRelease += DisableShield;
    }

    void EnableShield(){
        player.IsProtecting = true;
        shield.SetActive(true);
    }

    void DisableShield(){
        player.IsProtecting = false;
        shield.SetActive(false);
    }
}