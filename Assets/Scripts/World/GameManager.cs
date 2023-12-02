using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public static float GameSpeed = 1f;

    [SerializeField, Range(0f, 10f)]
    float gameSpeed = 1f;

    void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void OnValidate() {
        GameSpeed = gameSpeed;
    }
}