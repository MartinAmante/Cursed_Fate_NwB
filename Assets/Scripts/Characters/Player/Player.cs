using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Player : Character{
    private Vector3 playerSpawner;
    public static GameObject instance;

    private void Start(){
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        { //Se asegura que solo haya un objeto de este tipo
            instance = this.gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner").transform.position;
        transform.position = playerSpawner;
        GetComponent<Collider2D>().enabled = true;
        chara.IsAlive = true;
    }

    public override void CheckHealth(){
        if(chara.Health <= 0){
            chara.Lifes -= 1;
            if(chara.Lifes > 0){
                transform.position = playerSpawner;
                chara.Health = chara.MaxHealth;
                Debug.Log("has muerto");
            }else{
                GetComponent<Collider2D>().enabled = false;
                chara.IsAlive = false;
                Debug.Log("GAME OVER");
            }
        }
    }
}