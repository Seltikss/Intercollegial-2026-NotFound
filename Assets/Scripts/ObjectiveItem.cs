using System;
using UnityEngine;
using Player;

public class ObjectiveItem : MonoBehaviour
{
    public enum Types
    {
        MOTOR,
        OXYGEN_TANK,
        //Other thing
    }

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public Types itemType = 0;
    public Sprite[] typesSprites = new Sprite[2];


    private void Start()
    {
        spriteRenderer.sprite = typesSprites[(int) itemType];
    }


    public void PickUp()
    {
        Destroy(this.gameObject);
    }
}
