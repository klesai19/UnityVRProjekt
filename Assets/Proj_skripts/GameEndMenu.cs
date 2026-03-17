using UnityEngine;
using UnityEngine.SceneManagement;
public class GameEndMenu : MonoBehaviour
{
    public GameManager gm;
    
    public void ContinueGame()
    {
        gm.ToggleTempPanel();
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