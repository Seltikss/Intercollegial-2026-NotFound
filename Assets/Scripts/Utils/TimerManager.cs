using System;
using System.Collections.Generic;
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

        public UnityEvent onFinished = new UnityEvent();


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

    private List<string> timersIds = new List<string>(); 
    private List<Timer> timersArray = new List<Timer>();


    public void AddTimer(string id, float time)
    {
        timersIds.Add(id);
        Timer timer = new Timer(time);
        timersArray.Add(timer);
    }


    public Timer GetTimer(string id)
    {
        return timersArray[timersIds.IndexOf(id)];
    }
    

    public void StartTimer(string id)
    {
        timersArray[timersIds.IndexOf(id)].Start();
    }


    public bool IsStopped(string id)
    {
        return !timersArray[timersIds.IndexOf(id)].enabled;
    }
    

    private void Update()
    {
        for (int i = 0; i < timersArray.Count; i++)
        {
            if (timersArray[i].enabled)
            {
                timersArray[i].Update(Time.deltaTime);
            }
        }
    }
}
