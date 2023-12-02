using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI TimerText => timerText;

    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField, Range(0f, 10f)]
    float gameSpeed = 1f;

    [SerializeField, Min(0f)]
    float timeSlowFactor = 0.5f, timeSlowDuration = 1f, timeSlowCooldown = 1f;

    float timer = 0f;

    public bool isTimeSlowed = false;

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
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
        timerText.text = timeSpan.ToString(@"mm\:ss");
    }

    public void SlowTime() {
        if (isTimeSlowed)
            return;

        StartCoroutine(SlowTimeRoutine());
    }

    IEnumerator SlowTimeRoutine() {
        isTimeSlowed = true;
        Time.timeScale = timeSlowFactor;
        yield return new WaitForSecondsRealtime(timeSlowDuration);
        isTimeSlowed = false;
        Time.timeScale = gameSpeed;
    }
}