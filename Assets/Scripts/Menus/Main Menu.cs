using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class MainMenu : MonoBehaviour
{
    public static bool isFirstTime = true;
    
    [SerializeField] private string gameSceneName;
    public Button playButton;
    public Button SettingsButton;
    public Button dashButton;
    public Button reloadButton;
    public Button interactButton;
    public Button shootButton;
    private AudioSource source;
    public AudioClip soundEffect;

    [SerializeField] private GameObject settingMenu;


    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void AudioButton()
    {
        source.Play();
    }
    public void Play()
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }


    public void OnSettingClick()
    {
        settingMenu.SetActive(true);
    }

        
    public void Quit()
    {
        Application.Quit();
    }


    public void PlayButtonClick()
    {

    }
}
