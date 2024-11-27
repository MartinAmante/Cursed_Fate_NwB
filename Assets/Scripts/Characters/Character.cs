using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public abstract class Character : MonoBehaviour{
    [SerializeField] public CharacterData chara;
    [SerializeField] public GameObject canvasVida;
    public Slider healthBar;
    

    private void Awake()
    {
        if (gameObject.tag == "Enemy") 

        {
            chara = ScriptableObject.CreateInstance<CharacterData>();
            chara.Health = chara.MaxHealth;
            chara.Lifes = chara.MaxLifes;
        }
        else

        chara.Health = chara.MaxHealth;
        chara.Lifes = chara.MaxLifes;
        
    }

    void Update(){
        
    }

    public abstract void CheckHealth();

    public virtual void TakeDamage(int attackDamage){
        chara.Health -= attackDamage;
        CheckHealth();
    }
}