using System;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference dashInput;
    [SerializeField] private InputActionReference shootInput;
    [SerializeField] private InputActionReference reloadInput;
    
    [SerializeField] private Transform transform;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private PlayerData playerData;

    [SerializeField] private GameObject bulletInstance;
    
    //c_ mean constant in inspector. Vous pouvez le changer si cela vous dérange.
    [SerializeField] private int c_maxSpeed = 20;
    // [SerializeField] private int c_acceleration = 5;
    // [SerializeField] private int c_decelaration = 3;
    [SerializeField] private int c_dashForce = 30;
    [SerializeField] private float c_dashTime = 0.5f;
    
    private Vector2 velocity = Vector2.zero;

    private float nextDashTime = 0.0f;
    
    private bool hasShoot = false;
    private bool isDashing = false;


    private Vector2 GetInputDirection()
    {
        return moveInput.action.ReadValue<Vector2>();
    }


    private void Update()
    {
        //Make input buffer and put input here
    }


    private Vector2 GetMouseDirection()
    {
        Vector2 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        mouseDir.Normalize();
        return mouseDir;
    }


    private void ShootBullet()
    {
        playerData.bullet -= 1;
        GameObject bullet = Instantiate(bulletInstance, transform.position, new Quaternion());
        bullet.GetComponent<BulletController>().InitializeBullet(GetMouseDirection());
    }


    private void ReloadGun()
    {
        //Make function
    }


    private bool CanDash()
    {
        // isDashing
        return !isDashing && dashInput.action.IsPressed();
    }
    

    private void MovementProcess()
    {
        if (CanDash())
        {
            velocity = GetMouseDirection() * c_dashForce;
            nextDashTime = Time.time + c_dashTime;
            isDashing = true;
        }
        else if (isDashing && Time.time >= nextDashTime)
        {
            isDashing = false;
            
            Vector2 dir = velocity;
            dir.Normalize();
            velocity = dir * c_maxSpeed;
        }
        else if (!isDashing)
        {
            velocity = GetInputDirection() * c_maxSpeed;
        }
        
        rigidBody.MovePosition(rigidBody.position + (velocity * Time.fixedDeltaTime));
    }
    

    private void FixedUpdate()
    {
        MovementProcess();
        
        //Maybe input buffer
        if (shootInput.action.IsPressed() && !hasShoot)
        {
            hasShoot = true;
            ShootBullet();
        }
        else if (!shootInput.action.IsPressed() && hasShoot)
        {
            hasShoot = false;
        }
        
        if (reloadInput.action.IsPressed())
        {
            ReloadGun();
        }
    }
}
