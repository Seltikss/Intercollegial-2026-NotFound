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
    [SerializeField] private Vector2 c_scaleMod = new Vector2(0.5f, 0.5f);
    [SerializeField] private float c_time = 3f;
    
    
    public Types itemType = 0;
    public Sprite[] typesSprites = new Sprite[TYPE_NUM];
    public bool randomize = true;

    private float nextTime = -1.0f;


    private void Start()
    {
        if (randomize)
            itemType = (Types) Random.Range(0, TYPE_NUM);
        spriteRenderer.sprite = typesSprites[(int) itemType];
    }


    public void PickUp()
    {
        if (itemType == Types.EASTER_EGG)
        {
            Debug.Log("Picked UPP");
            nextTime = Time.time + c_time;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Update()
    {
        if (nextTime == -1.0)
            return;

        if (nextTime <= Time.time)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.localScale += new Vector3(c_scaleMod.x, c_scaleMod.y, 0.0f) * Time.deltaTime;
        }
    }
}
