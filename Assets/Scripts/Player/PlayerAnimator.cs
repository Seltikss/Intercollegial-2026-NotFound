using System;
using Player;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerInput playerInput;


    private void Update()
    {
        if (playerInput.moveVector.x != 0)
            spriteRenderer.flipX = playerInput.moveVector.x < 0;
        
        
    }
}
