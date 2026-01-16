using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Utils
{
    public class AudioManager : MonoBehaviour
    {
        public enum Musics
        {
            A,
            B,
            C,
            D,
        }
        
        public static AudioManager instance;

        [SerializeField] private GameObject sourceInstance;
        [SerializeField] private AudioSource musicPlayer;

        private List<AudioSource> sources = new List<AudioSource>();

        public AudioClip hurtPlayer;
        public AudioClip deathPlayer;
        public AudioClip doorOpen;
        public AudioClip dashPlayer;
        public AudioClip gunShot;
        public AudioClip itemFound;


        private void Start()
        {
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


        public void PlayMusic(Musics index)
        {
            
        }


        private void Update()
        {
            for (int i = 0; i < sources.Count; i++)
            {
                if (!sources[i].isPlaying)
                {
                    Destroy(sources[i].gameObject);
                    sources.RemoveAt(i);
                }
            }
        }
    }
}