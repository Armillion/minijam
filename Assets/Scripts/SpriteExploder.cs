using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteExploder : MonoBehaviour {
    [SerializeField]
    List<Rigidbody2D> spritePieces = new();

    [SerializeField]
    float explosionForce = 1000f;

    [SerializeField]
    private SoundSystem ss;

    void Start() {
        ss = GameObject.FindWithTag("Finish").GetComponent<SoundSystem>();
        ss.clauses[1] = true;
        foreach (var spriteRB in spritePieces)
            spriteRB.AddForce(Random.insideUnitCircle * explosionForce, ForceMode2D.Impulse);
    }
}
