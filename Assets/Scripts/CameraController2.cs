using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public Camera cameraPlayer;
    public GameObject player;
    Vector3 playermov;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("INICIO");
        playermov = new Vector3(player.transform.position.x, player.transform.position.y, cameraPlayer.transform.position.z);
        cameraPlayer.transform.position = playermov;
    }
}