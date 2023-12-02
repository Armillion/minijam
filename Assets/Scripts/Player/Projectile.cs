using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 9999999999999f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(speed*Time.deltaTime, 0);
        dir = transform.rotation * dir;
        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("aaaaaaaa");
        if (other.gameObject.TryGetComponent<Entity>(out var en))
        {
            en.freeze();
        }

        if (!other.CompareTag("Player"))
            Destroy(gameObject);
    }
}
