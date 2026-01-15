using System;
using Player;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerInput playerInput;

        [SerializeField] private GameObject gunObject;
        [SerializeField] private SpriteRenderer gunSpriteRenderer;

        [SerializeField] private Vector2 c_gunOffset = new Vector2(.1f, .1f);
        [SerializeField] private float c_rotCutoff = 0.2f;


        private void Update()
        {
            animator.SetBool("isMoving", playerInput.moveVector != Vector2.zero);
            if (playerInput.facingVector.y < -c_rotCutoff)
                animator.SetInteger("dir", 0);
            else if (playerInput.facingVector.y > c_rotCutoff)
                animator.SetInteger("dir", 1);
            else
                animator.SetInteger("dir", 2);
            
            spriteRenderer.flipX = playerInput.facingVector.x < 0;
            gunSpriteRenderer.flipY = playerInput.facingVector.x < 0;

            gunSpriteRenderer.sortingLayerName = playerInput.facingVector.y > c_rotCutoff ? "Layer1" : "Layer3";

            Vector2 offset = c_gunOffset * playerInput.facingVector;
            gunObject.transform.position = transform.position + new Vector3(offset.x, offset.y, 0);
            gunObject.transform.eulerAngles =
                new Vector3(0, 0, Mathf.Rad2Deg * (float) Math.Atan2(playerInput.facingVector.y, playerInput.facingVector.x));
        }
    }
}
