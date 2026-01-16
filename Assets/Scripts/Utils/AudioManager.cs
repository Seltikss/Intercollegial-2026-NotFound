using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Utils
{
    public class AudioManager : MonoBehaviour
    {
        public static bool isFirstTime = true;
        
        public enum Musics
        {
            NONE = -1,
            MAIN_THEME,
            ALUMINIUM, 
            BISMUTH, //Gameplay
            NEO, //Gameplay
            STEEL, //Credit
            TITAN //GAmeplay
        }
        
        public static AudioManager instance;

        [SerializeField] private GameObject sourceInstance;
        [SerializeField] private AudioSource musicPlayer;
        [SerializeField] private AudioClip[] musicClips = new AudioClip[6];

        private List<AudioSource> sources = new List<AudioSource>();

        public AudioClip hurtPlayer;
        public AudioClip deathPlayer;
        public AudioClip dashPlayer;
        public AudioClip gunShot;
        public AudioClip itemFound;
        public AudioClip darkButton;
        public AudioClip doorOpen;

        public Musics musicPlaying = Musics.NONE;


        private void Start()
        {
            if (instance != null)
                return;
            
            instance = this;
            DontDestroyOnLoad(this);
        }


        public void Play(AudioClip clip, Transform t)
        {
            GameObject obj = Instantiate(sourceInstance, t);
            AudioSource source = obj.GetComponent<AudioSource>();
            sources.Add(source);
            source.clip = clip;
            source.Play();
        }


        public void PlayMusic(Musics index, bool loop, Transform t)
        {
            if (index == Musics.NONE || index == musicPlaying)
                return;

            musicPlaying = index;
            Debug.Log("Test");
            musicPlayer.loop = loop;
            musicPlayer.clip = musicClips[(int)index];
            musicPlayer.transform.position = t.position;
            musicPlayer.Play();
        }


        private void Update()
        {
            for (int i = 0; i < sources.Count; i++)
            {
                if (sources[i] == null)
                    continue;
                
                if (!sources[i].isPlaying)
                {
                    Destroy(sources[i].gameObject);
                    sources.RemoveAt(i);
                }
            }
        }
    }
}