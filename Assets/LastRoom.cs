using Player;
using UnityEngine;

public class LastRoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerData>().enteredLastRoom = true;
    }
}
