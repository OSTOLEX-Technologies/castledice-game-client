using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static EventHandler<bool> OnSceneLoadingStarted;
    
    public void LoadScene(string sceneName, bool bAsyncMode = false)
    {
        OnSceneLoadingStarted?.Invoke(this, bAsyncMode);
        if (bAsyncMode)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
