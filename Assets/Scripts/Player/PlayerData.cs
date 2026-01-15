using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        const string IMMUNITY_TIMER_ID = "Immunity Timer";
    
        public const int MAX_HEALTH = 10;
        public const int MAX_BULLET = 10;

        public UnityEvent onPlayerKilled = new UnityEvent();
    
        [SerializeField] private TimerManager timerManager;
    
        [SerializeField] private float c_immunityTime = 0.5f;
    
        [HideInInspector] public int health { get; private set; } = MAX_HEALTH;
        [HideInInspector] public int bullet = MAX_BULLET;
    
        private bool[] hasObjectiveItems = new bool[2]; //Mettre cela de la mème size de ObjectiveItem.Type


        private void Start()
        {
            timerManager.AddTimer(IMMUNITY_TIMER_ID, c_immunityTime);
        }


        public void TakeDamage(int damage)
        {
            if (!timerManager.IsStopped(IMMUNITY_TIMER_ID))
                return;
        
            health -= damage;
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Remove this !!
                onPlayerKilled.Invoke();
            }
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


        public void PickUpObjectiveItem(ObjectiveItem item)
        {
            hasObjectiveItems[(int) item.itemType] = true;
            item.PickUp();
        }
    }
}
