using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public System.Action LoadScene(int SceneBuildIndex, LoadSceneMode sceneLoadMode)
    {
        SceneManager.LoadScene(SceneBuildIndex, sceneLoadMode);
        return null;
    }
    public System.Action LoadScene(string SceneName, LoadSceneMode sceneLoadMode)
    {
        SceneManager.LoadScene(SceneName, sceneLoadMode);
        return null;
    }
}
