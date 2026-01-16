using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class GameOver : MonoBehaviour
{
    private AudioSource source;
    public AudioClip soundEffect;

    [SerializeField] private string MainSceneName;
    [SerializeField] private string GameSceneName;

    [SerializeField] private AudioSource audioSource;


    public Button mainMenuButton;
    public Button tryAgainButton;

    public void PlayAgain()
    {
        AudioManager.instance.Play(AudioManager.instance.darkButton, transform);
        SceneManager.LoadScene(GameSceneName);
    }

    public void GoToMenu()
    {
        AudioManager.instance.Play(AudioManager.instance.darkButton, transform);
        SceneManager.LoadScene(MainSceneName);
    }
}
