using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{
    [SerializeField]public CharacterData chara;
    private Slider healthBar;

    void Start(){
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = chara.MaxHealth;
    }

    void Update(){
        healthBar.value = chara.Health;
    }
}