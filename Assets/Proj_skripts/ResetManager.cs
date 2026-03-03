using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetManager : MonoBehaviour
{

    [SerializeField] private GameObject resetPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    public void ToggleResetPanel()
    {
        if (resetPanel == null) return;
        resetPanel.SetActive(!resetPanel.activeSelf);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}