using System;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private const int DASH_DURATION_TIMER_ID = 0;
    private const int DASH_COOLDOWN_TIMER_ID = 1;
    private const int GUN_COOLDOWN_TIMER_ID = 2;
    private const int RELOAD_DURATION_TIMER_ID = 3;
    
    [SerializeField] private GameObject bulletInstance;
    
    [Header("Input Action References")]
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference dashInput;
    [SerializeField] private InputActionReference shootInput;
    [SerializeField] private InputActionReference reloadInput;
    
    [Header("Components")]
    [SerializeField] private Transform transform;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TimerManager timerManager;
    
    [Header("Variable")]
    [SerializeField] private int c_maxSpeed = 20;
    [SerializeField] private int c_dashForce = 30;
    [SerializeField] private float c_dashTime = 0.5f;
    [SerializeField] private float c_dashCooldown = 0.5f;
    
    [SerializeField] private float c_gunCooldown = 0.5f;
    [SerializeField] private float c_reloadDuration = 0.5f;
    
    private Vector2 velocity = Vector2.zero;
    
    private bool hasShoot = false;
    private bool isDashing = false;


    private void Start()
    {
        timerManager.SetTimerTime(DASH_DURATION_TIMER_ID, c_dashTime);
        timerManager.SetTimerTime(DASH_COOLDOWN_TIMER_ID, c_dashCooldown);
        timerManager.SetTimerTime(GUN_COOLDOWN_TIMER_ID, c_gunCooldown);
        timerManager.SetTimerTime(RELOAD_DURATION_TIMER_ID, c_reloadDuration);
    }


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
        timerManager.StartTimer(GUN_COOLDOWN_TIMER_ID);
        playerData.bullet -= 1;
        GameObject bullet = Instantiate(bulletInstance, transform.position, new Quaternion());
        bullet.GetComponent<BulletController>().InitializeBullet(GetMouseDirection());
    }


    private void ReloadGun()
    {
        if (timerManager.IsStopped(RELOAD_DURATION_TIMER_ID))
            timerManager.StartTimer(RELOAD_DURATION_TIMER_ID);
    }


    private bool CanShoot()
    {
        return shootInput.action.IsPressed() && timerManager.IsStopped(GUN_COOLDOWN_TIMER_ID);
    }


    private bool CanDash()
    {
        // isDashing
        return !isDashing && dashInput.action.IsPressed() && timerManager.IsStopped(DASH_COOLDOWN_TIMER_ID);
    }
    

    private void MovementProcess()
    {
        if (CanDash())
        {
            velocity = GetInputDirection() * c_dashForce;
            // nextDashTime = Time.time + c_dashTime;
            timerManager.StartTimer(DASH_DURATION_TIMER_ID);
            isDashing = true;
        }
        else if (isDashing && timerManager.IsStopped(DASH_DURATION_TIMER_ID))
        {
            isDashing = false;
            
            Vector2 dir = velocity;
            dir.Normalize();
            velocity = dir * c_maxSpeed;
            
            timerManager.StartTimer(DASH_COOLDOWN_TIMER_ID);
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
        if (CanShoot())
        {
            if (playerData.HasBullet())
                ShootBullet();
            else
                ReloadGun();
        }
        else if (reloadInput.action.IsPressed())
        {
            ReloadGun();
        }
    }
}
