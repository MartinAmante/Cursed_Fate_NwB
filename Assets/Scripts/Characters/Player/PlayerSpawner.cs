using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float playerCount;
    //[SerializeField] public GameObject playerFinal;
    // Start is called before the first frame update
    private void Awake()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
            if ( playerCount >= 1) 
        {
            Debug.Log("Hay" + playerCount + " Players");
            player.SetActive(false);
        }
            if (playerCount == 0)
        {
            player.SetActive(true);
        }
        
    }
    void Start()
    {
        Instantiate(player);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        //player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
    }
}
