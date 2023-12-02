using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Entity en = other.gameObject.GetComponent<Entity>();
        if (en != null)
        {
            en.onHit();
        }
    }
}
