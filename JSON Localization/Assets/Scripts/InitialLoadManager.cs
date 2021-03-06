using SimianX.Localization;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsible for validating a set of manager instances.
/// Will transition to the next scene after all instances are flagged as ready
/// </summary>
public class InitialLoadManager : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;

    private IEnumerator Start()
    {
        while (!LocalizationManager.Instance.Ready)
        {
            yield return null;
        }
        SceneManager.LoadScene(nextSceneName);
    }
}
