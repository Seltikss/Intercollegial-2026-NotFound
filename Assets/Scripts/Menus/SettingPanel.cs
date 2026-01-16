using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingPanel : MonoBehaviour
{
    public InputActionReference dashInput;
    public InputActionReference shootInput;
    public InputActionReference reloadInput;
    public InputActionReference interactInput;
    public InputActionReference pauseInput;
    public TMP_Text dashText;
    public TMP_Text shootText;
    public TMP_Text reloadText;
    public TMP_Text interactText;
    public TMP_Text pauseText;
    [SerializeField] private GameObject SettingMenu;

    private void Update()
    {
        dashText.text = dashInput.action.GetBindingDisplayString();
        shootText.text = shootInput.action.GetBindingDisplayString();
        reloadText.text = reloadInput.action.GetBindingDisplayString();
        interactText.text = interactInput.action.GetBindingDisplayString();
        pauseText.text = pauseInput.action.GetBindingDisplayString();
    }
    public void OnDashRebindClicked()
    {
        OnAction(dashInput.action);
        dashText.text = dashInput.action.GetBindingDisplayString();
        dashText.text = dashInput.action.GetBindingDisplayString();

    }

    public void OnPauseRebindClicked()
    {
        OnAction(pauseInput.action);
        pauseText.text = dashInput.action.GetBindingDisplayString();
        pauseText.text = dashInput.action.GetBindingDisplayString();

    }

    public void OnShootRebindClicked()
    {
        OnAction(shootInput.action);
        shootText.text = shootInput.action.GetBindingDisplayString();
        shootText.text = shootInput.action.GetBindingDisplayString();
    }


    public void OnReloadRebindClicked()
    {
        OnAction(reloadInput.action);
        reloadText.text = reloadInput.action.GetBindingDisplayString();
        reloadText.text = reloadInput.action.GetBindingDisplayString();
    }


    public void OnInteractRebindClicked()
    {
        OnAction(interactInput.action);
        interactText.text = interactInput.action.GetBindingDisplayString();
        interactText.text = interactInput.action.GetBindingDisplayString();
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
