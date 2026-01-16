using System;
using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    private static int staticHealth = -1;
    private static int staticPoison = -1;
    private static int staticScore = -1;
    
    public static GuiController instance;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider poisonSlider;
    [SerializeField] private Image[] bulletImages = new Image[PlayerData.MAX_BULLET];
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private InputActionReference pauseMenuInput;
    
    [SerializeField] private GameObject pauseScreen;

    private bool pauseMenuInputPressed = false;


    private void Start()
    {
        if (staticHealth != -1)
        {
            SetHealth(staticHealth);
            staticHealth = -1;
        }
        if (staticPoison != -1)
        {
            SetPoison(staticPoison);
            staticPoison = -1;
        }
        if (staticScore != -1)
        {
            SetScore(staticScore);
            staticScore = -1;
        }
        
        instance = this;
        pauseScreen.SetActive(false);
    }


    public void OnRetryButton()
    {
        // SceneManager.LoadScene(1);
    }
    
    
    public void OnGoBackButton()
    {
        Debug.Log("Test");
        SceneManager.LoadScene(0);
    }
    
    
    public void EnablePauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    
    public void DisablePauseScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }


    public static void SetStaticHealth(int health)
    {
        if (instance)
            instance.SetHealth(health);
        else
        {
            staticHealth = health;
        }
    }
    
    
    public static void SetStaticPoison(int poison)
    {
        if (instance)
            instance.SetPoison(poison);
        else
            staticPoison = poison;
    }
    
    
    public static void SetStaticScore(int score)
    {
        if (instance)
            instance.SetScore(score);
        else
            staticScore = score;
    }


    public void SetHealth(int health)
    {
        health = health <= 0 ? 0 : health;
        healthSlider.value = ((float)health) / ((float)PlayerData.MAX_HEALTH);
    }
    
    
    public void SetPoison(int poison)
    {
        poison = poison <= 0 ? 0 : poison;
        poisonSlider.value = ((float) poison) / ((float)PlayerData.MAX_POISON);
    }


    public void SetBulletLeft(int left)
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            bulletImages[i].enabled = i < left;
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = $"SCRAPS SCAVENGED : {score}";
    }


    public void Update()
    {
        if (pauseMenuInput.action.IsPressed() && !pauseMenuInputPressed)
        {
            if (pauseScreen.activeSelf)
                DisablePauseScreen();
            else
                EnablePauseScreen();
            pauseMenuInputPressed = true;
        }
        else if (!pauseMenuInput.action.IsPressed())
        {
            pauseMenuInputPressed = false;
        }
            
    }
}
