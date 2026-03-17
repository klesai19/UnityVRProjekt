using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public string sceneName;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
