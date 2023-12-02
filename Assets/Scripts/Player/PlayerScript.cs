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

        if (Input.GetButtonDown("Fire2"))
        {
            Melee();
        }
    }

    void FixedUpdate()
    {
        playerMovement.Move(horizontal * Time.fixedUnscaledDeltaTime, false, jump);
        jump = false;
    }

    void Melee()
    {
        Debug.Log("Melee");
        var colliders = Physics2D.OverlapBoxAll(transform.position + (playerMovement.m_FacingRight ? 1f : -1f) * new Vector3(attackRange.x * 0.5f, 0f, 0f), attackRange, 0f, LayerMask.GetMask("Enemy"));
        
        foreach (var collider in colliders)
        {
            Debug.Log($"Hit: {collider.gameObject.name}");

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (playerMovement.m_FacingRight ? 1f : -1f) * new Vector3(attackRange.x * 0.5f, 0f, 0f), attackRange);
    }
}
