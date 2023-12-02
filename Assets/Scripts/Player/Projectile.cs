using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(speed*Time.deltaTime, 0);
        dir *= transform.forward;
        rb.velocity = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("aaaaaaaa");
        Entity en = other.gameObject.GetComponent<Entity>();
        if (en != null)
        {
            en.freeze();
        }
        Destroy(gameObject);
    }
}
