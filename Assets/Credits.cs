using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Credits : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int sceneIndex = 0;
    private float nextTime = 0.0f;
    
    void Start()
    {
        nextTime = Time.time + 1.0f;
        videoPlayer.Play();
    }


    private void Update()
    {
        if (!videoPlayer.isPlaying && nextTime <= Time.time)
            SceneManager.LoadScene(sceneIndex);
    }
}
