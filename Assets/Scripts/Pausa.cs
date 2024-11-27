using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    [SerializeField] private Camera cameraPlayer;
    [SerializeField] private GameObject pausa;
    // Start is called before the first frame update
    void Start()
    {
        pausa.transform.position = gameObject.transform.position;
        cameraPlayer = FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
            pausa.transform.position = new Vector3 (cameraPlayer.transform.position.x, cameraPlayer.transform.position.y, pausa.transform.position.z); 
    }
}
