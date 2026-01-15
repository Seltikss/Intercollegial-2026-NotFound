using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    public static GuiController instance;
    
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider poisonSlider;
    
    [SerializeField] private float c_healthBarLimit = 0.5f;
    [SerializeField] private float c_poisonBarLimit = 0.5f;


    private void Start()
    {
        instance = this;
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
}
