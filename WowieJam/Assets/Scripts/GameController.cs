using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject playerPrefab;
    Vector3 playerSpawn = new Vector3(-6.0f, -3.5f, 0.0f);

    public GameObject startMenu;

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
    }

    public void GameOver()
    {
        startMenu.SetActive(true);
    }
}
