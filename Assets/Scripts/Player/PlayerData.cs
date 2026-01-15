using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    const int IMMUNITY_TIMER_ID = 4;
    
    public const int MAX_HEALTH = 10;
    public const int MAX_BULLET = 10;

    [SerializeField] private TimerManager timerManager;
    
    [SerializeField] private float c_immunityTime = 0.5f;

    public UnityEvent onPlayerKilled;
    
    [HideInInspector] public int health { get; private set; } = MAX_HEALTH;
    [HideInInspector] public int bullet = MAX_BULLET;


    public void TakeDamage(int damage)
    {
        if (!timerManager.IsStopped(IMMUNITY_TIMER_ID))
            return;
        
        health -= damage;
        if (health <= 0)
            onPlayerKilled.Invoke();
        else
            timerManager.StartTimer(IMMUNITY_TIMER_ID);
    }
    

    public void ResetHealth()
    {
        health = MAX_HEALTH;
    }


    public bool HasBullet()
    {
        return bullet > 0;
    }
    

    public void ResetBullet()
    {
        bullet = MAX_BULLET;
    }
}
