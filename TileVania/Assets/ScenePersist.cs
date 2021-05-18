using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    private void Awake()
    {
        int numGameSession = FindObjectsOfType<ScenePersist>().Length;
        if (numGameSession > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void GotoNextLevel()
    {
        Destroy(gameObject);
    }
}
