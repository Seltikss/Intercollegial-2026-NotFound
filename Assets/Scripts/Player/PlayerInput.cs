using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private const string DASH_BUFFER_TIMER_ID = "DASH_BUFFER";
        private const string RELOAD_BUFFER_TIMER_ID = "RELOAD_BUFFER";
        private const string INTERACT_BUFFER_TIMER_ID = "INTERACT_BUFFER";
        
        [Header("Input Action References")]
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference dashInput;
        [SerializeField] private InputActionReference shootInput;
        [SerializeField] private InputActionReference reloadInput;
        [SerializeField] private InputActionReference interactInput;

        [SerializeField] private TimerManager timerManager;
        [SerializeField] private float c_dashBufferTime = 0.5f;
        [SerializeField] private float c_reloadBufferTime = 0.5f;
        [SerializeField] private float c_interactBufferTime = 0.5f;
        
        public Vector2 moveVector => moveInput.action.ReadValue<Vector2>();
        public Vector2 nonZeroMoveVector = Vector2.right;
        public Vector2 facingVector = Vector2.right;
        [HideInInspector] public bool isDashPressed { get; private set; } = false;
        [HideInInspector] public bool isShootPressed { get; private set; } = false;
        [HideInInspector] public bool isReloadPressed { get; private set; } = false;
        [HideInInspector] public bool isInteractPressed { get; private set; } = false;
        
        [HideInInspector] public bool isDashJustPressed => !timerManager.IsStopped(DASH_BUFFER_TIMER_ID);
        [HideInInspector] public bool isReloadJustPressed => !timerManager.IsStopped(RELOAD_BUFFER_TIMER_ID);
        [HideInInspector] public bool isInteractJustPressed  => !timerManager.IsStopped(INTERACT_BUFFER_TIMER_ID);


        private void Start()
        {
            timerManager.AddTimer(DASH_BUFFER_TIMER_ID, c_dashBufferTime);
            timerManager.AddTimer(RELOAD_BUFFER_TIMER_ID, c_reloadBufferTime);
            timerManager.AddTimer(INTERACT_BUFFER_TIMER_ID, c_interactBufferTime);
        }


        public Vector2 GetMouseDirection()
        {
            Vector2 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            mouseDir.Normalize();
            return mouseDir;
        }


        private void Update()
        {
            if (moveVector != Vector2.zero)
                nonZeroMoveVector = moveVector;

            facingVector = GetMouseDirection();

            if (!isDashPressed && dashInput.action.IsPressed())
                timerManager.StartTimer(DASH_BUFFER_TIMER_ID);
            if (!isReloadPressed && reloadInput.action.IsPressed())
                timerManager.StartTimer(RELOAD_BUFFER_TIMER_ID);
            if (!isInteractPressed && interactInput.action.IsPressed())
                timerManager.StartTimer(INTERACT_BUFFER_TIMER_ID);
            
            isDashPressed = dashInput.action.IsPressed();
            isShootPressed = shootInput.action.IsPressed();
            isReloadPressed = reloadInput.action.IsPressed();
            isInteractPressed = interactInput.action.IsPressed();
        }
    }
}