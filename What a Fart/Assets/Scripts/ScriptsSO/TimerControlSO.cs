using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptsSO
{
    [CreateAssetMenu(fileName = "TimeController",menuName = "ScriptableObject/TimerControlSO",order = 1)]
    public class TimerControlSO : ScriptableObject
    {
        public float timePerLevel;
        [SerializeField] private float defaultTime;
        public bool isRunningLevel;
        [System.NonSerialized]
        public UnityEvent<float> TimeLevelEvent;
        private void OnEnable()
        {
            timePerLevel = defaultTime;
            if (TimeLevelEvent == null)
                TimeLevelEvent = new UnityEvent<float>();
        }

        public IEnumerator CounterTime()
        {
            while (timePerLevel >0)
            {
                TimeLevelEvent.Invoke(timePerLevel);
                yield return new WaitForSeconds(1);
                timePerLevel--;
            }

            isRunningLevel = false;
        }
    }
}