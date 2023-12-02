using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public static float GameSpeed = 1f;

    void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}