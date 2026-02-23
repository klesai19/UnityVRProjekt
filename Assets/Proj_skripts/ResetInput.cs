using UnityEngine;
using UnityEngine.InputSystem;

public class ResetInput : MonoBehaviour
{
    [SerializeField] private InputActionReference toggleResetAction;
    [SerializeField] private ResetManager resetManager;

    private void OnEnable()
    {
        toggleResetAction.action.Enable();
        toggleResetAction.action.performed += OnToggle;
    }

    private void OnDisable()
    {
        toggleResetAction.action.performed -= OnToggle;
        toggleResetAction.action.Disable();
    }

    private void OnToggle(InputAction.CallbackContext ctx)
    {
        resetManager.ToggleResetPanel();
    }
}