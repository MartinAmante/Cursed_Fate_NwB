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
        }
        else

        chara.Health = chara.MaxHealth;
        chara.Lifes = chara.MaxLifes;
        
    }

    void Update(){
        if (gameObject.tag == "Player")

        {
            canvasVida = GameObject.FindGameObjectWithTag("PlayerSlider");
            healthBar = canvasVida.GetComponentInChildren<Slider>();
        }
        healthBar.maxValue = chara.MaxHealth;
        healthBar.value = chara.Health;
        chara.IsWaiting = !(!chara.IsAlive || chara.IsWalking || chara.IsAttacking || chara.IsAttackingFire || chara.IsDashing || chara.IsProtecting);
        if(!chara.IsAttacking && !chara.IsAttackingFire ){
            if(chara.IsTurning){
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }else{
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public abstract void CheckHealth();

    public virtual void TakeDamage(int attackDamage){
        chara.Health -= attackDamage;
        CheckHealth();
    }
}