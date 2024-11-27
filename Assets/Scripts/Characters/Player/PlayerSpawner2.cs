using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner2 : MonoBehaviour
{

    [SerializeField] private GameObject player;
    //[SerializeField] public GameObject playerFinal;
    // Start is called before the first frame update
    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
    }
}
