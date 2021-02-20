using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for validating all singleton manager instances. Will transition to the "DemoJSON" scene after all instances are flagged as ready
/// </summary>
public class LoadingScreenManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (!LocalizationManager.Instance.Ready)
        {
            yield return null;
        }
        SceneManager.LoadScene("DemoJSON");
    }
}
