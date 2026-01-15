using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }


    public void OnSettingClick()
    {
        
    }


    public void OnSettingBack()
    {
        
    }

        
    public void Quit()
    {
        Application.Quit();
    }
}
