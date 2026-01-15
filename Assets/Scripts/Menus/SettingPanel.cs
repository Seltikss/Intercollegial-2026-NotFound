using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingPanel : MonoBehaviour
{
    public InputActionReference dashInput;
    public InputActionReference shootInput;
    public InputActionReference reloadInput;
    public InputActionReference interactInput;
    [SerializeField] private GameObject SettingMenu;

    public void OnDashRebindClicked()
    {
        OnAction(dashInput.action);
    }


    public void OnShootRebindClicked()
    {
        OnAction(shootInput.action);
    }


    public void OnReloadRebindClicked()
    {
        OnAction(reloadInput.action);
    }


    public void OnInteractRebindClicked()
    {
        OnAction(interactInput.action);
    }

    public void OnBackButtonClicked()
    {
        SettingMenu.SetActive(false);
    }


    void OnAction(InputAction inputAction)
    {
        inputAction.Disable();
        inputAction.PerformInteractiveRebinding()
            .Start();
        inputAction.Enable();
    }
}
