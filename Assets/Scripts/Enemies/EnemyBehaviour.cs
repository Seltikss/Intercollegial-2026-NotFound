using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int DETECTION_RADIUS = 10;
    public string playerTag = "Player";
    public float enemySpeed = 1.0f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
    }


    void FixedUpdate()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        hits = Physics2D.CircleCastAll(transform.position, DETECTION_RADIUS, Vector2.up);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag(playerTag))
            {
                Move(hits[i].transform.position);
                break;
            }
        }
    }

    private void Move(Vector3 playerPos)
    {
        Vector3 dir = (playerPos - transform.position);
        dir.Normalize();
        Vector3 velocity = dir * enemySpeed * Time.fixedDeltaTime;
        transform.position += velocity;
    }
}
