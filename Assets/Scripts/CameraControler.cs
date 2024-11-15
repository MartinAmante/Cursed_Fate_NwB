using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    public Camera cameraPlayer;
    public GameObject player;
    Vector3 playermov;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        playermov = new Vector3(player.transform.position.x, player.transform.position.y, cameraPlayer.transform.position.z);
        cameraPlayer.transform.position = playermov;
    }
}