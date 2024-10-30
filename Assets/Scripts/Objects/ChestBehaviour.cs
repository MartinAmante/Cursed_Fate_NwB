using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [SerializeField] private CharacterData player;
    [SerializeField] private ChestsData chest;
    [SerializeField] private GameObject aura;
    [SerializeField] private GameObject parchments;
    [SerializeField] private Transform spawnPoint;


    public void ChestOpen()
    {
        if (player.IsNearChest && !player.IsInteracting)
        {
            player.IsInteracting = true;
            StartCoroutine(Opening());
        }
    }
    IEnumerator Opening()
    {
        Debug.Log("Cofre abierto");
        chest.IsOpening = true;
        yield return new WaitForSeconds(chest.CooldownInteract);
        aura.SetActive(true);
        parchments.SetActive(true);
        yield return new WaitForSeconds(chest.CooldownAnimation);

        int randomParchment = Random.Range(0, chest.ParchmentList.Count);

        // Obtén el GameObject en el índice aleatorio
        GameObject selectedParchment = chest.ParchmentList[randomParchment];

        // Instancia el objeto en el punto deseado con su rotación original
        Instantiate(selectedParchment, spawnPoint.position, Quaternion.identity);
        aura.SetActive(false);
        parchments.SetActive(false);
        player.IsInteracting = false;
        chest.Islooted = true;
    }
}
