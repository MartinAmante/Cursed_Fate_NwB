using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Player : Character{
    [SerializeField] private GameObject playerSpawner;
    [SerializeField] private string id;
    public static GameObject instance;


    private void Awake()
    {
        
    }
    private void Start(){
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner");
        transform.position = playerSpawner.transform.position;
        GetComponent<Collider2D>().enabled = true;
        chara.IsAlive = true;
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        { //Se asegura que solo haya un objeto de este tipo
            instance = this.gameObject;
        }
        id = gameObject.GetInstanceID().ToString();
        if (id != "403348")
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        canvasVida = GameObject.FindGameObjectWithTag("PlayerSlider");
        healthBar = canvasVida.GetComponentInChildren<Slider>();
        healthBar.maxValue = chara.MaxHealth;
        healthBar.value = chara.Health;
        chara.IsWaiting = !(!chara.IsAlive || chara.IsWalking || chara.IsAttacking || chara.IsAttackingFire || chara.IsDashing || chara.IsProtecting);
        if (!chara.IsAttacking && !chara.IsAttackingFire)
        {
            if (chara.IsTurning)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner");
    }

    public override void CheckHealth(){
        if(chara.Health <= 0){
            chara.Lifes -= 1;
            if(chara.Lifes > 0){
                transform.position = playerSpawner.transform.position;
                chara.Health = chara.MaxHealth;
                Debug.Log("has muerto");
            }else{
                GetComponent<Collider2D>().enabled = false;
                chara.IsAlive = false;
                PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("SceneMuerte");

            }
        }
    }

}