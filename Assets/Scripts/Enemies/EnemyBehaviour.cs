using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int DETECTION_RADIUS = 10;
    private ContactFilter2D filter = new ContactFilter2D();
    public string playerTag = string.Empty;
    public Vector3 playerPos = Vector3.zero;
    public float enemySpeed = 1.0f;

    void FixedUpdate()
    {
        playerPos = Vector2.zero;
        RaycastHit2D[] hits = new RaycastHit2D[10];
        hits = Physics2D.CircleCastAll(transform.position, DETECTION_RADIUS, Vector2.up);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag(playerTag))
            {
                playerPos = hits[i].transform.position;
                break;
            }
        }
        if (playerPos != Vector3.zero)
            Move(playerPos);

    }

    private void Move(Vector3 playerPos)
    {
        Vector3 dir = (playerPos - transform.position);
        dir.Normalize();
        Vector3 velocity = dir * enemySpeed * Time.fixedDeltaTime;
        transform.position += velocity;
    }
}
