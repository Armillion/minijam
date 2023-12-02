using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI TimerText => timerText;

    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField, Range(0f, 10f)]
    float gameSpeed = 1f;

    float timer = 0f;

    void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void OnValidate() {
        Time.timeScale = gameSpeed;
    }

    void Update() {

        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2");
    }
}