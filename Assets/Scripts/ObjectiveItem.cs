using System;
using UnityEngine;
using Player;
using Random = UnityEngine.Random;

public class ObjectiveItem : MonoBehaviour
{
    public enum Types
    {
        ANTIDOTE,
        GAZ_CANISTER,
        CASSETTE,
        EASTER_EGG,
        ESSENCE,
        WIRE,
        GOURVENAIL,
        MARTEAU,
        TV,
        VIS
    }

    public const int TYPE_NUM = 10; 

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public Types itemType = 0;
    public Sprite[] typesSprites = new Sprite[TYPE_NUM];


    private void Start()
    {
        Debug.Log(Random.Range(0, TYPE_NUM));
        itemType = (Types) Random.Range(0, TYPE_NUM);
        spriteRenderer.sprite = typesSprites[(int) itemType];
    }


    public void PickUp()
    {
        Destroy(this.gameObject);
    }
}
