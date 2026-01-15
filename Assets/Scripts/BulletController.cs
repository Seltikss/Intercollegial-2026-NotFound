using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private const int SPEED = 1;
    
    [SerializeField] private Transform transform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Vector2 direction = Vector2.zero;


    public void InitializeBullet(Vector2 dir)
    {
        direction = dir;
        float rotation = Mathf.Rad2Deg * (float) Math.Atan2(dir.y, dir.x);
        transform.eulerAngles = new Vector3(0, 0, rotation);
        
        spriteRenderer.enabled = true;
        enabled = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        
        switch (other.gameObject.tag)
        {
            case "Ground":
                Destroy(other.gameObject);
                break;
            case "Enemy":
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
                Destroy(other.gameObject);
                break;
        }
    }


    private void FixedUpdate()
    {
        Vector2 velocity = direction * SPEED * Time.fixedDeltaTime;
        transform.position += new Vector3(velocity.x, velocity.y, 0);
    }
}
