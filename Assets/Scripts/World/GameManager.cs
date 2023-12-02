using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI TimerText => timerText;

    [SerializeField]
    MonoBehaviour worldScroller;

    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField, Range(0f, 10f)]
    float gameSpeed = 1f;

    [SerializeField, Min(0f)]
    float timeSlowFactor = 0.5f, timeSlowDuration = 1f, timeSlowCooldown = 1f;

    public bool isTimeSlowed = false;

    [SerializeField, Space, Min(0.1f)]
    float startCooldown = 5f;

    public float Timer { get; set; }
    
    float cooldown = 0f;

    void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        worldScroller.enabled = false;
        StartCoroutine(StartCooldown());
    }

    void OnValidate() {
        Time.timeScale = gameSpeed;
    }

    void Update() {
        Timer += Time.deltaTime;
        cooldown -= Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(Timer);
        timerText.text = timeSpan.ToString(@"mm\:ss");
    }

    public void SlowTime() {
        if (isTimeSlowed || timeSlowCooldown > 0f)
            return;

        StartCoroutine(SlowTimeRoutine());
    }

    IEnumerator SlowTimeRoutine() {
        isTimeSlowed = true;
        Time.timeScale = timeSlowFactor;
        timerText.color = Color.cyan;
        yield return new WaitForSecondsRealtime(timeSlowDuration);
        isTimeSlowed = false;
        Time.timeScale = gameSpeed;
        timerText.color = Color.black;
        cooldown = timeSlowDuration;
    }

    IEnumerator StartCooldown() {
        yield return new WaitForSeconds(startCooldown);
        worldScroller.enabled = true;
    }
}