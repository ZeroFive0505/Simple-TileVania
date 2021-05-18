using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManage : MonoBehaviour
{
    private GameObject Exit;
    // Start is called before the first frame update
    void Start()
    {
        Exit = GameObject.Find("Exit Level");
        Exit.SetActive(false);
    }

    public void OnClick()
    {
        Exit.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
