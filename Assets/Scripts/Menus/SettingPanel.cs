using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingPanel : MonoBehaviour
{
    public InputActionReference actionRef;
    
    private void Start()
    {
        OnAction(actionRef.action);
    }

    void OnAction(InputAction inputAction)
    {
        inputAction.Disable();
        inputAction.PerformInteractiveRebinding()
            .Start();
        inputAction.Enable();
    }
}
