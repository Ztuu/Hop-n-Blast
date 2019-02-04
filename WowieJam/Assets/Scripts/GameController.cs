using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject playerPrefab, enemySpawnerPrefab;
    Vector3 playerSpawn = new Vector3(-6.0f, -3.5f, 0.0f);

    public GameObject startMenu;
    public Text scoreText, highScoreText;

    GameObject enemySpawner;

    int score = 0;
    int highScore = 0;

    //TODO: Set up event system for initially selected buttons
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }else
        {
            instance = this;
        }

        startMenu.SetActive(true);
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        enemySpawner = Instantiate(enemySpawnerPrefab, Vector3.zero, Quaternion.identity);
        score = 0;
        scoreText.text = score + "";
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score + "";
    }

    public void GameOver()
    {
        startMenu.SetActive(true);
        Destroy(enemySpawner);
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = highScore + "";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
