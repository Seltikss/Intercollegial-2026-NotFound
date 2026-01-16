using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource source;
    public AudioClip soundEffect;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayButtonClick()
    {
        source.Play();
    }
}
