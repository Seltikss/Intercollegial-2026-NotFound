using System;
using UnityEngine;
using Utils;

public class EnemyHealth : MonoBehaviour
{
    private int health = 3;

    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(this.gameObject);
            AudioManager.instance.Play(AudioManager.instance.hurtPlayer, transform);


    }
}
