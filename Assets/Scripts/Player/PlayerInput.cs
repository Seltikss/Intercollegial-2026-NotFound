using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

namespace Player
{
    public class PlayerInput : MonoBehaviour 
    {
        [Header("Input Action References")]
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference dashInput;
        [SerializeField] private InputActionReference shootInput;
        [SerializeField] private InputActionReference reloadInput;
        [SerializeField] private InputActionReference interactInput;

        public Vector2 moveVector => moveInput.action.ReadValue<Vector2>();
        public bool isDashPressed { get; private set; } = false;
        public bool isShootPressed { get; private set; } = false;
        public bool isReloadPressed { get; private set; } = false;
        public bool isInteractPressed { get; private set; } = false;
        
        public bool isDashJustPressed { get; private set; } = false;
        public bool isReloadJustPressed { get; private set; } = false;
        public bool isInteractJustPressed { get; private set; } = false;
        
        


        private void Update()
        {
            isDashJustPressed = !isDashPressed && dashInput.action.IsPressed();
            isReloadJustPressed = !isReloadPressed && reloadInput.action.IsPressed();
            isInteractJustPressed = !isInteractPressed && interactInput.action.IsPressed();
            
            isDashPressed = dashInput.action.IsPressed();
            isShootPressed = shootInput.action.IsPressed();
            isReloadPressed = reloadInput.action.IsPressed();
            isInteractPressed = interactInput.action.IsPressed();
        }
    }
}