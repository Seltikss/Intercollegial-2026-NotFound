using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
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
        SceneManager.LoadScene(gameSceneName);
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
