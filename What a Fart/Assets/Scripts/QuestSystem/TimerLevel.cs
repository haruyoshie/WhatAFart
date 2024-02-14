using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerLevel : MonoBehaviour
{
    public float totalTimePerTask = 60f; // Total time for the timer in seconds
    public float totalTimePerLevel = 60f; // Total time for the timer in seconds
    private float _currentTime,_currentTimeLevel; // Current time left
    private bool _isTimerRunning = false;
    private bool _isTimerRunningLevel = false;

    public TextMeshProUGUI timerText,timerTextLevel; // Text component to display the timer

    IEnumerator Start()
    {
        
        _currentTime = totalTimePerTask;
        _currentTimeLevel = totalTimePerLevel;
        timerText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        _isTimerRunningLevel = true;
        UpdateTimerDisplayLevel();
    }

    void Update()
    {
        if (_isTimerRunningLevel)
        {
            _currentTimeLevel -= Time.deltaTime;
            UpdateTimerDisplayLevel();
            if (_currentTimeLevel <= 0)
            {
                _currentTimeLevel = 0f;
                _isTimerRunningLevel = false;
                StopTimer();
            }
            if (_isTimerRunning)
            {
                _currentTime -= Time.deltaTime; // Decrease current time by deltaTime
                UpdateTimerDisplay();

                if (_currentTime <= 0f)
                {
                    _currentTime = 0f;
                    _isTimerRunning = false;
                    StopTimer();
                    // Perform any actions you want when the timer reaches 0
                    Debug.Log("Timer has reached 0.");
                }
            }
        }
       
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);
        string timerString = $"{minutes:00}:{seconds:00}";
        timerText.text = "Time to complete the task: "+ timerString;
    }
    void UpdateTimerDisplayLevel()
    {
        int minutes = Mathf.FloorToInt(_currentTimeLevel / 60f);
        int seconds = Mathf.FloorToInt(_currentTimeLevel % 60f);
        string timerString = $"{minutes:00}:{seconds:00}";
        timerTextLevel.text = "Time per level: "+ timerString;
    }
    public void StartTimer()
    {
        _isTimerRunning = true;
        timerText.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        FindObjectOfType<Controller>().LoseForTime();
        _isTimerRunning = false;
    }
}

