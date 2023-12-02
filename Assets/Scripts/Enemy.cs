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
    [SerializeField] float explosionTreshold = 5f;
    [SerializeField] Transform iceCubesPrefab;
    float prevVelMagnitude;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isFrozen)
        {
            float delta = prevVelMagnitude - rb.velocity.magnitude;

            if (delta > explosionTreshold)
                onDeath();

            prevVelMagnitude = rb.velocity.magnitude;
            return;
        }

        Vector2 vel = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = vel;

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

    public override void onHit(Vector3 force)
    {
        if (!isFrozen)
        {
            return;
        }

        base.onHit(force);
        rb.AddForce(force, ForceMode2D.Impulse);
        prevVelMagnitude = rb.velocity.magnitude;
    }

    public override void onDeath()
    {
        base.onDeath();
        var iceCubes = Instantiate(iceCubesPrefab, transform.position, Quaternion.identity);
        iceCubes.localScale = transform.localScale;
        GameManager.Instance.Timer += 3f;
        Destroy(gameObject);
    }
}
