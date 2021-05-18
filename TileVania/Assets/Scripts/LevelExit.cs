using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {
        Time.timeScale = LevelExitSlowMoFactor;
        FindObjectOfType<ScenePersist>().GotoNextLevel();
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

}
