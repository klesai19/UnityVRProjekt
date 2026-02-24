using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void EndGame()
    {
     
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif
    }
}