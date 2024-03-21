using System;
using System.Collections;
using System.Collections.Generic;
using ScriptsSO;
using TMPro;
using UnityEngine;

public class ManagerTimer : MonoBehaviour
{
    public TimerControlSO timerControlSo;

    public TextMeshProUGUI timerVisual;

    private string timerText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timerControlSo.CounterTime());
        
    }

    private void OnEnable()
    {
        timerControlSo.TimeLevelEvent.AddListener(UpdateTextTimer);
    }
    

    public void UpdateTextTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timerText = $"{minutes:00}:{seconds:00}";
        timerVisual.text = timerText;
    }
}
