using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenereloader : MonoBehaviour
{
  public void ReloadScene()
  {
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currentScene.name);
  }
}
