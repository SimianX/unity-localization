using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Generic UI methods to load scenes
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Loads a scene included in the project's build index
    /// </summary>
    /// <param name="sceneBuildIndex">
    /// Index of the built scene
    /// </param>
    public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    /// <summary>
    /// Loads a scene included in the project's build index
    /// </summary>
    /// <param name="sceneName">
    /// Name of the built scene
    /// </param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Triggers Unity's Quit process
    /// </summary>
    public void TriggerQuit()
    {
        Application.Quit();
    }
}
