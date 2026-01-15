using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    [SerializeField] private GameObject settingMenu;

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
}
