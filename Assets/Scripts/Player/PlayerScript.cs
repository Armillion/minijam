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

    public GameObject gameOverScreen;

    public override void onHit()
    {
        base.onHit();

        injuries++;
        // apply knockback
        Vector3 lookAtPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        int dir = transform.position.x > lookAtPosition.x ? -1 : 1;
        float force = 100f * dir;
        playerMovement.knockback(force * Time.fixedUnscaledDeltaTime);
        jump = false;
    }

    public override void onFall()
    {
        base.onFall();
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
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
        playerMovement.Move(horizontal * Time.fixedUnscaledDeltaTime, false, jump);
        jump = false;
    }
}
