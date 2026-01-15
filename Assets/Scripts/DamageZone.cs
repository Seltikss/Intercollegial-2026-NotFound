using System;
using UnityEngine;
using Player;

public class DamageZone : MonoBehaviour
{
    public int damage = 1;
    public bool dashInvulnerability = false;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerData playerData))
        {
            if (dashInvulnerability && other.gameObject.GetComponent<PlayerController>().isDashing)
                return;
            
            playerData.TakeDamage(damage);
        }
    }
}
