<<<<<<< Updated upstream
using NUnit.Framework;
using Player;
using System.Collections.Generic;
=======
using Player;
>>>>>>> Stashed changes
using UnityEngine;

public class Door : MonoBehaviour
{
<<<<<<< Updated upstream


    public Sprite openDoor;
    public bool isOpen = false;
    public float walkInSpeed = 8;
    public int doorType = 0;

    private List<Vector2> directions = new List<Vector2> { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
=======
    

    public Sprite openDoor;
    public bool isOpen = false;
    void Update()
    {
        
    }
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
            collision.GetComponent<PlayerController>().isLocked = true;
            collision.GetComponent<PlayerController>().SetVelocity(directions[doorType] * walkInSpeed);
=======
            collision.GetComponent<PlayerInput>().freezeMotherfucker();
>>>>>>> Stashed changes
        }
    }

}
