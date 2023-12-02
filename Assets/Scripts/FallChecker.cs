using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity en = other.gameObject.GetComponent<Entity>();
        if (en != null)
        {
            en.onFall();
        }
    }
}
