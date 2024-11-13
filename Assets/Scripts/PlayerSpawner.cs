using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour{
    private Transform player;

    void Start(){
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        player.position = gameObject.transform.position;
    }
}