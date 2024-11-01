using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Character : MonoBehaviour{
    [SerializeField]public CharacterData chara;

    private void Awake(){
        chara.Health = chara.MaxHealth;
    }

    void Update(){
        chara.IsWaiting = !(!chara.IsAlive || chara.IsWalking || chara.IsAttacking || chara.IsDashing || chara.IsProtecting);
        if(!chara.IsAttacking){
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