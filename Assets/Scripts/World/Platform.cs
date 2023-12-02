using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    [SerializeField]
    SpriteRenderer modelSprite;

    [SerializeField, Min(0.1f)]
    Vector2 size;

    public Vector2 Size {
        get => modelSprite.size;
        set {
            modelSprite.size = new Vector2(value.x, value.y);
            boxCollider.size = new Vector2(value.x, value.y);
        }
    }

    BoxCollider2D boxCollider;

    void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnValidate() {
        boxCollider = GetComponent<BoxCollider2D>();
        Size = size;
    }
}
