using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Timer : Entity {
    public override void freeze()
    {
        base.freeze();
        GameManager.Instance.SlowTime();
    }
}
