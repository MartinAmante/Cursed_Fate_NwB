using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [SerializeField] public CharacterData player;
    public delegate void DirectionalInput(float xAxis, float yAxis);
    public delegate void DashInput();
    public delegate void AttackInput();
    public delegate void ProtectInput();
    public delegate void ProtectReleaseInput();
    public delegate void InteractInput();
    public delegate void InteractChestInput();
    public static DirectionalInput direction;
    public static DashInput dash;
    public static AttackInput attack;
    public static ProtectInput protect;
    public static ProtectReleaseInput protectRelease;
    public static InteractInput interact;
    public static InteractChestInput interactChest;

    void Update() {
        if (player.IsAlive && direction != null)
        {
            Inputs();
        }
    }

    void Inputs()
    {
        direction(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );
        if (Input.GetButton("Dash")) dash();
        if (Input.GetButton("Attack")) attack();
        if (Input.GetButton("Protect")) { protect(); } else { protectRelease(); }
        if (Input.GetButton("Interact")) interact();
        if (Input.GetButton("Interact")) interactChest();
    }
}
