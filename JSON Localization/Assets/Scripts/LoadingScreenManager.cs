using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
