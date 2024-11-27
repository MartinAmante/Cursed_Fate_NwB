using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] public GameObject player;
    //[SerializeField] public GameObject playerFinal;
    // Start is called before the first frame update
    private void Awake()
    {
        //Instantiate(playerFinal);
    }
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
