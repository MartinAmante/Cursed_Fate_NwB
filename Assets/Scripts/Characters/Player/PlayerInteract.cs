using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private CharacterData player;
    //[SerializeField] private ParchmentsData parchment;
    [SerializeField] GameObject currentParchment;
    [SerializeField] private float runAnimation;

    void Start()
    {
        PlayerInput.interact += Interact;       
    }

    // Update is called once per frame
    void Interact()
    {
        
        if (player.IsNearParchment && !player.IsInteracting)
        {
            player.IsInteracting = true;
            if (currentParchment != null)
            {
                powerUp powerUpComponent = currentParchment.GetComponent<powerUp>();
                if (powerUpComponent != null)
                {
                    ParchmentsData parchmentData = powerUpComponent.GetParchmentData();
                    if (parchmentData != null)
                    {
                        StartCoroutine(ApplyPowerUp(parchmentData));
                        Destroy(currentParchment);
                        currentParchment = null;
                    }
                }
            }
        }
    }
    private IEnumerator ApplyPowerUp(ParchmentsData parchment)
    {
        float originalMoveSpeed = player.MoveSpeed;
        float originalNormalSpeed = player.NormalSpeed;
        int originalHealt = player.Health;
        int originalMaxHealth = player.MaxHealth;
        float originalAttackSpeed = player.WeaponList[player.Weapon].AttackSpeed;
        float originalCooldowSpeed = player.WeaponList[player.Weapon].AttackCooldown;
        int originalAttackDamage = player.WeaponList[player.Weapon].AttackDamage;

        switch (parchment.NewPowerUpType)
        {
            case PowerUpType.Speed:
                player.MoveSpeed = parchment.MoveSpeed;
                player.NormalSpeed = parchment.NormalSpeed;
                break;
            case PowerUpType.Armor:
                player.Health = parchment.Health;
                player.MaxHealth = parchment.MaxHealth;
                break;
            case PowerUpType.AttackSpeed:
                player.WeaponList[player.Weapon].AttackSpeed = parchment.AttackSpeed;
                player.WeaponList[player.Weapon].AttackCooldown = parchment.CooldownSpeed; 
                break;
            case PowerUpType.FireSword:
                player.IsOnFire = true;
                player.WeaponList[player.Weapon].AttackDamage = parchment.AttackDamage;
                break;
        }
        yield return new WaitForSeconds(parchment.Duration);
        player.MoveSpeed = originalMoveSpeed;
        player.NormalSpeed = originalNormalSpeed;
        player.Health = originalHealt;
        player.MaxHealth = originalMaxHealth;
        player.WeaponList[player.Weapon].AttackSpeed = originalAttackSpeed;
        player.WeaponList[player.Weapon].AttackCooldown = originalCooldowSpeed;
        player.IsInteracting = false;
        player.IsOnFire = false;
        player.WeaponList[player.Weapon].AttackDamage = originalAttackDamage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            player.IsNearParchment = true;
            currentParchment = other.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            player.IsNearParchment = false; // El jugador se aleja del pergamino
            currentParchment = null;
        }
    }
}
