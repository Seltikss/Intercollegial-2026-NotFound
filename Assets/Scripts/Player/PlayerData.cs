using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public const int MAX_HEALTH = 10;
    public const int MAX_BULLET = 10;
    
    public int health = MAX_HEALTH;
    public int bullet = MAX_BULLET;


    public void ResetHealth()
    {
        health = MAX_HEALTH;
    }


    public void ResetBullet()
    {
        bullet = MAX_BULLET;
    }
}
