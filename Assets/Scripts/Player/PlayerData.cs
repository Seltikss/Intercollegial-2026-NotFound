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
        const string POISON_TIMER_ID = "Poison Timer";
        private const string POISON_TAG = "Poison";
    
        public const int MAX_HEALTH = 10;
        public const int MAX_BULLET = 10;
        public const int MAX_POISON = 50;

        public UnityEvent onPlayerKilled = new UnityEvent();
    
        [SerializeField] private TimerManager timerManager;
    
        [SerializeField] private float c_immunityTime = 0.5f;
        [SerializeField] private float c_poisonTime = 0.5f;
    
        [HideInInspector] public int health { get; private set; } = MAX_HEALTH;
        [HideInInspector] public int bullet = MAX_BULLET;
        [HideInInspector] public int poison = 0;
    
        private bool[] hasObjectiveItems = new bool[ObjectiveItem.TYPE_NUM]; //Mettre cela de la mème size de ObjectiveItem.Type
        private bool isInPoison = false;
        

        private void Start()
        {
            timerManager.AddTimer(IMMUNITY_TIMER_ID, c_immunityTime);
            timerManager.AddTimer(POISON_TIMER_ID, c_poisonTime);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(POISON_TAG))
                isInPoison = true;
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(POISON_TAG))
                isInPoison = false;
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


        private void FixedUpdate()
        {
            if (timerManager.IsStopped(POISON_TIMER_ID))
            {
                if (isInPoison && poison < MAX_POISON)
                {
                    poison++;
                    timerManager.StartTimer(POISON_TIMER_ID);
                }
                else if (!isInPoison && poison > 0)
                {
                    poison--;
                    timerManager.StartTimer(POISON_TIMER_ID);
                }
            }
            
            if (poison == MAX_POISON)
                TakeDamage(1);
        }
    }
}
