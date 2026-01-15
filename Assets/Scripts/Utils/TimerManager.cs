using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    [System.Serializable]
    public class Timer
    {
        public bool enabled { get; private set; } = false;
        public float time;
        public float timeLeft { get; private set; } = 0.0f;

        public UnityEvent onFinished;


        public Timer(float t)
        {
            this.time = t;
        }


        public void Start()
        {
            timeLeft = time;
            enabled = true;
        }
        
        
        public void Update(float t)
        {
            timeLeft -= t;
            if (timeLeft <= 0)
            {
                enabled = false;
                onFinished.Invoke();
            }
        }
    }
    
    public Timer[] timersArray = {};


    public void SetTimerTime(int id, float time)
    {
        timersArray[id].time = time;
    }
    

    public void StartTimer(int id)
    {
        timersArray[id].Start();
    }


    public bool IsStopped(int id)
    {
        return !timersArray[id].enabled;
    }
    

    private void Update()
    {
        for (int i = 0; i < timersArray.Length; i++)
        {
            if (timersArray[i].enabled)
            {
                timersArray[i].Update(Time.deltaTime);
            }
        }
    }
}
