using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : Entity
{
    public float platformWidth = 0;             // determines for far an enemy can go
    float currentWidth = 0;                     // derermines our current position on the platform
    [SerializeField] private float speed = 10;  // how fast enemy moves
    private int direction = -1;
    private Rigidbody2D rb;
    
    public Collider2D attackChecker;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isFrozen)
        {
            Vector2 vel = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
            rb.velocity = vel;
        }
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }

        currentWidth += direction * speed * Time.deltaTime;

        if(currentWidth > platformWidth || currentWidth < -platformWidth)
        {
            direction *= -1;
            Flip();
        }
    }

    public override void freeze()
    {
        base.freeze();
        attackChecker.enabled = false;
        animator.SetBool("isFrozen", true);
    }

    public override void onFall()
    {
        base.onFall();
        Destroy(gameObject);
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
