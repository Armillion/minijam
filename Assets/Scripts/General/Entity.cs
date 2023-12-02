using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected bool isFrozen = false;

    public virtual void freeze()
    {
        isFrozen = true;
        // what also happens depends on tyle of entity being frozen
    }

    public virtual void onHit()
    {
        // what happens while being hit depends on tyle of entity being frozen
    }

    public virtual void onHit(Vector3 force)
    {
        // what happens while being hit depends on tyle of entity being frozen
    }

    public virtual void onFall()
    {
        // determines what happens when we fall off the world
    }
}
