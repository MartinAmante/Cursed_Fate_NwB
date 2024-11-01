using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour{
    [SerializeField]private CharacterData chara;
    [SerializeField]private List<AudioClip> audioList = new List<AudioClip>();
    public AudioSource controlSonido;
    public AudioClip attackSound;
    public AudioClip slideSound;

    void Update()
    {
        Action();
    }
    public void Action(){
        if(chara.IsAttacking && !controlSonido.isPlaying) controlSonido.PlayOneShot(attackSound);
        if(chara.IsDashing && !controlSonido.isPlaying) controlSonido.PlayOneShot(slideSound);
    }
}
