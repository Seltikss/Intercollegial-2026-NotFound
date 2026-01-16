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
    public static GuiController instance;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider poisonSlider;
    [SerializeField] private Image[] bulletImages = new Image[PlayerData.MAX_BULLET];
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private InputActionReference pauseMenuInput;
    
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;

    private bool pauseMenuInputPressed = false;


    private void Start()
    {
        instance = this;
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
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

    
    public void EnableWinScreen()
    {
        winScreen.SetActive(true);
    }
    
    
    public void EnableGameOverScreen()
    {
        gameOverScreen.SetActive(true);
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
        scoreText.text = $"SCRAPS SCAVENGED : {0}";
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
