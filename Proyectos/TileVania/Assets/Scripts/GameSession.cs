 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int initialScore = 0;
    int totalScore = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        if(numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = initialScore.ToString();
    }

    public void ProcessCoinPickup(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1) 
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        scoreText.text = initialScore.ToString();
        totalScore = initialScore;
        SceneManager.LoadScene(0);
        FindObjectOfType<ScenePersist>().DestroyScenePersist();
        Destroy(gameObject);
    }
}
