using System;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
{
    private const string DASH_DURATION_TIMER_ID = "DASH_DUR_TIMER";
    private const string DASH_COOLDOWN_TIMER_ID = "DASH_COOL_TIMER";
    private const string GUN_COOLDOWN_TIMER_ID = "GUN_TIMER";
    private const string RELOAD_DURATION_TIMER_ID = "RELOAD_TIMER";
    
    [SerializeField] private GameObject bulletInstance;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private Door doorManager;
    
    [Header("Variable")]
    [SerializeField] private int c_maxSpeed = 20;
    [SerializeField] private int c_dashForce = 30;
    [SerializeField] private float c_dashTime = 0.5f;
    [SerializeField] private float c_dashCooldown = 0.5f;
    
    [SerializeField] private float c_gunCooldown = 0.5f;
    [SerializeField] private float c_reloadDuration = 0.5f;
    
    [SerializeField] private float c_interactRadius = 0.5f;
    [SerializeField] private float c_interactDist = 0.5f;
    
    private Vector2 velocity = Vector2.zero;
    public bool isLocked = false;
    [HideInInspector] public bool isDashing { get; private set; } = false;


    private void Start()
    {
        timerManager.AddTimer(DASH_DURATION_TIMER_ID, c_dashTime);
        timerManager.AddTimer(DASH_COOLDOWN_TIMER_ID, c_dashCooldown);
        timerManager.AddTimer(GUN_COOLDOWN_TIMER_ID, c_gunCooldown);
        timerManager.AddTimer(RELOAD_DURATION_TIMER_ID, c_reloadDuration);
        timerManager.GetTimer(RELOAD_DURATION_TIMER_ID).onFinished.AddListener(playerData.ResetBullet);
    }


    private void ShootBullet()
    {
        timerManager.StartTimer(GUN_COOLDOWN_TIMER_ID);
        playerData.bullet -= 1;
        GameObject bullet = Instantiate(bulletInstance, transform.position, new Quaternion());
        bullet.GetComponent<BulletController>().InitializeBullet(playerInput.GetMouseDirection());
    }


    private void ReloadGun()
    {
        if (timerManager.IsStopped(RELOAD_DURATION_TIMER_ID))
            timerManager.StartTimer(RELOAD_DURATION_TIMER_ID);
    }


    private void Interact()
    {
        Vector2 pos = transform.position.ConvertTo<Vector2>() + playerInput.GetMouseDirection() * c_interactDist;
        var result = Physics2D.OverlapCircleAll(pos, c_interactRadius);
        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] && result[i].TryGetComponent(out ObjectiveItem item))
            {
                playerData.PickUpObjectiveItem(item);
            }
            else if (result[i].transform.CompareTag("Door"))
            {
                doorManager.OpenDoor();
            }
        }
    }


    private bool CanShoot()
    {
        return playerInput.isShootPressed && timerManager.IsStopped(GUN_COOLDOWN_TIMER_ID);
    }


    private bool CanDash()
    {
        // isDashing
        return !isDashing && playerInput.isDashPressed && timerManager.IsStopped(DASH_COOLDOWN_TIMER_ID);
    }


    public void SetVelocity(Vector2 dir)
    {
        velocity = c_maxSpeed * Time.fixedDeltaTime * dir;
    }


    private void ApplyVelocity()
    {
        if (CanDash())
        {
            velocity = playerInput.facingVector * c_dashForce;
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
            velocity = playerInput.moveVector * c_maxSpeed;
        }
    }
    
    
    private void MovementProcess()
    {
        if (!isLocked)
            ApplyVelocity();
        
        rigidBody.MovePosition(rigidBody.position + (velocity * Time.fixedDeltaTime));
    }
    

    private void FixedUpdate()
    {
        MovementProcess();
        
        if (CanShoot())
        {
            if (playerData.HasBullet())
                ShootBullet();
            else
                ReloadGun();
        }
        else if (playerInput.isReloadJustPressed)
        {
            ReloadGun();
        }
        else if (playerInput.isInteractJustPressed)
        {
            Interact();
        }
    }
}
}

