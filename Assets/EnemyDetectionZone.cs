using UnityEngine;

public class EnemyDetectionZone : MonoBehaviour
{
    public bool isInRange = false;
    public Vector3 playerPos = Vector3.zero;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            playerPos = collision.transform.position;
        }
    }
}
