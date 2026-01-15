using Player;
using UnityEngine;

public class Door : MonoBehaviour
{
    

    public Sprite openDoor;
    public bool isOpen = false;
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        transform.GetComponent<SpriteRenderer>().sprite = openDoor;
        isOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen && collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<CircleCollider2D>().isTrigger = true;
            collision.GetComponent<PlayerInput>().freezeMotherfucker();
        }
    }

}
