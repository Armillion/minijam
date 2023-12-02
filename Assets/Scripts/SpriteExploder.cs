using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteExploder : MonoBehaviour {
    [SerializeField]
    List<Rigidbody2D> spritePieces = new();

    [SerializeField]
    float explosionForce = 1000f;

    void Start() {
        foreach (var spriteRB in spritePieces)
            spriteRB.AddForce(Random.insideUnitCircle * explosionForce, ForceMode2D.Impulse);
    }
}
