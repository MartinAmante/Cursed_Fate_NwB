using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractChest : MonoBehaviour
{
    [SerializeField] private CharacterData player; 
    [SerializeField] private ChestsData chest; 
    [SerializeField] private ChestBehaviour chestBehaviour;
    void Start()
    {
        chest.Islooted = false;
        PlayerInput.interactChest += Interact;
        //chestBehaviour = FindObjectOfType<ChestBehaviour>();
    }

    private void Update()
    {
        if(chest.IsOpening && !player.IsNearChest)
        {
            chest.IsOpening = false;
        }
    }

    // Update is called once per frame
    void Interact()
    {
        if(!chest.Islooted)
        {
            chestBehaviour.ChestOpen();
        }
           
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            player.IsNearChest = true; // El jugador está cerca del cofre
            Debug.Log("Jugador cerca del cofre");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            player.IsNearChest = false; // El jugador se aleja del cofre
            Debug.Log("Jugador se aleja del cofre");
        }
    }
}
