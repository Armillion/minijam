using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity
{
    // Class handles player specific operations

    [SerializeField] private int injuries = 0;

    public PlayerMovement playerMovement;

    public float runSpeed = 40f;

    float horizontal = 0f;
    public bool jump = false;

    public override void onHit()
    {
        base.onHit();

        injuries++;
        // apply knockback
        playerMovement.Move(horizontal * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public override void onFall()
    {
        base.onFall();
        Debug.Log("Game Over");
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        playerMovement.Move(horizontal * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
