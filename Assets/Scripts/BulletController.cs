using System;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private const int SPEED = 1;
    
    [SerializeField] private Transform transform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private float c_collRadius = 1f;
    
    private Vector2 direction = Vector2.zero;
    public bool isEnabled = false;


    public void InitializeBullet(Vector2 dir)
    {
        direction = dir;
        float rotation = Mathf.Rad2Deg * (float) Math.Atan2(dir.y, dir.x);
        transform.eulerAngles = new Vector3(0, 0, rotation);
        
        spriteRenderer.enabled = true;
        enabled = true;
        isEnabled = true;
    }


    private void FixedUpdate()
    {
        if (!isEnabled)
            return;
        
        Vector2 velocity = direction * SPEED * Time.fixedDeltaTime;
        transform.position += new Vector3(velocity.x, velocity.y, 0);

        var results = Physics2D.OverlapCircleAll(transform.position, c_collRadius);
        for (int i = 0; i < results.Length; i++)
        {
            Debug.Log(results[i].gameObject.tag);
            switch (results[i].gameObject.tag)
            {
                case "Ground":
                    Destroy(gameObject);
                    break;
                case "Enemy":
                    results[i].gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
