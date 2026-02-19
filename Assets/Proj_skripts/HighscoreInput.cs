using UnityEngine;
using UnityEngine.InputSystem;
public class HighscoreInput : MonoBehaviour
{
    [SerializeField] private InputActionReference toggleHighscoreAction;
    [SerializeField] private HighscoreManager highscoreManager;
    private void OnEnable()
    {
        if (toggleHighscoreAction != null)
        {
            toggleHighscoreAction.action.Enable();
            toggleHighscoreAction.action.performed += OnToggle;
        }
    }
    private void OnDisable()
    {
        if (toggleHighscoreAction != null)
        {
            toggleHighscoreAction.action.performed -= OnToggle;
            toggleHighscoreAction.action.Disable();
        }
    }
    private void OnToggle(InputAction.CallbackContext ctx)
    {
        if (highscoreManager != null)
            highscoreManager.TogglePanel();
    }
}