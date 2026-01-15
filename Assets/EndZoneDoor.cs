using Player;
using UnityEngine;

public class EndZoneDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CircleCollider2D>().isTrigger = false;
            collision.GetComponent<PlayerController>().isLocked = false;
        }
    }
}
