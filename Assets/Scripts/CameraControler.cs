using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cameraPlayer;
    public GameObject player;
    Vector3 playermov;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playermov = new Vector3(player.transform.position.x, player.transform.position.y, cameraPlayer.transform.position.z);
        cameraPlayer.transform.position = playermov;
    }
}
