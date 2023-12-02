using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity
{
    // Class handles player specific operations

    [SerializeField] private int injuries = 0;

    [SerializeField] Vector2 attackRange;

    [SerializeField, Min(0f)] float hammerForce = 500f;

    public PlayerMovement playerMovement;

    public float runSpeed = 40f;

    float horizontal = 0f;
    
    public bool jump = false;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;

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

        if (Input.GetButtonDown("Fire2"))
        {
            Melee();
        }

        if(Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
    }

    void FixedUpdate()
    {
        playerMovement.Move(horizontal * Time.fixedUnscaledDeltaTime, false, jump);
        jump = false;
    }

    void Melee()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        bool facingRight = mousePos.x > objectPos.x;

        var colliders = Physics2D.OverlapBoxAll(transform.position + (facingRight ? 1f : -1f) * new Vector3(attackRange.x * 0.5f, 0f, 0f), attackRange, 0f, LayerMask.GetMask("Enemy"));
        
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Entity>(out var en))
            {
                Vector3 dir = en.transform.position - transform.position;
                Vector2 force = hammerForce * dir;
                en.onHit(force);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        bool facingRight = mousePos.x > objectPos.x;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (facingRight ? 1f : -1f) * new Vector3(attackRange.x * 0.5f, 0f, 0f), attackRange);
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }
}
