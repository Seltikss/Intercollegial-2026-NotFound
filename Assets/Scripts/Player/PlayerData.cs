using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        const string IMMUNITY_TIMER_ID = "Immunity Timer";
        const string POISON_TIMER_ID = "Poison Timer";
        private const string POISON_TAG = "Poison";
    
        public const int MAX_HEALTH = 10;
        public const int MAX_BULLET = 5;
        public const int MAX_POISON = 50;
        private AudioSource source;
        public AudioClip soundEffect;

        public UnityEvent onPlayerKilled = new UnityEvent();
    
        [SerializeField] private TimerManager timerManager;
        [SerializeField] private AudioSource audioSource;
    
        [SerializeField] private float c_immunityTime = 0.5f;
        [SerializeField] private float c_poisonTime = 0.5f;
    
        [HideInInspector] public int health = MAX_HEALTH;
        [HideInInspector] private int bullet  = MAX_BULLET;
        [HideInInspector] public int poison = 0;
    
        [SerializeField] private bool[] hasObjectiveItems = new bool[ObjectiveItem.TYPE_NUM]; //Mettre cela de la mème size de ObjectiveItem.Type
        private bool isInPoison = false;

        public bool enteredLastRoom = false;
        public int totalScore = 0;
        

        private void Start()

        {
            timerManager.AddTimer(IMMUNITY_TIMER_ID, c_immunityTime);
            timerManager.AddTimer(POISON_TIMER_ID, c_poisonTime);
            
            GuiController.instance.SetHealth(health);
            GuiController.instance.SetPoison(poison);

            source = GetComponent<AudioSource>();
            
            DontDestroyOnLoad(gameObject);
            PlayerInput[] playerData = FindObjectsOfType<PlayerInput>();
            if (playerData.Length > 1)
                Destroy(gameObject);
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
            //AudioManager.instance.Play(AudioManager.insatance.hurtPlayer, transform);
            GuiController.instance.SetHealth(health);
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
            GuiController.instance.SetHealth(health);
        }


        public bool HasBullet()
        {
            return bullet > 0;
        }


        public void RemoveBullet()
        {
            bullet -= 1;
            GuiController.instance.SetBulletLeft(bullet);
        }
    

        public void ResetBullet()
        {
            bullet = MAX_BULLET;
            GuiController.instance.SetBulletLeft(bullet);
        }


        public void PickUpObjectiveItem(ObjectiveItem item)
        {
            hasObjectiveItems[(int) item.itemType] = true;
            item.PickUp();
        }

        public void CompletedRun()
        {
            int score = 0;
            for (int i = 0; i < hasObjectiveItems.Length; i++)
                if (hasObjectiveItems[i] == true)
                {
                    hasObjectiveItems[i] = false;
                    score++;    
                }

            totalScore += score;
            enteredLastRoom = false;


            // load new scene
            SceneManager.LoadScene("StartScene");
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
                GuiController.instance.SetPoison(poison);
            }
            
            if (poison == MAX_POISON)
                TakeDamage(1);
        }

        public void PlayHurt()
        {
            source.Play();
        }
    }
}
