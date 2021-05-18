using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerlives = 3;
    [SerializeField] Text liveText;
    [SerializeField] Text coinText;
    private int pickUp_coins = 0;

    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        updateStatus();
    }

    private void updateStatus()
    {
        liveText.text = "Lives : " + playerlives.ToString();
        coinText.text = "Coins : " + pickUp_coins.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerlives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void pickUpCoins()
    {
        pickUp_coins++;
        updateStatus();
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerlives--;
        updateStatus();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
