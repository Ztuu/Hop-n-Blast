using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject playerPrefab;
    Vector3 playerSpawn = new Vector3(-6.0f, -3.5f, 0.0f);

    GameObject startMenu;

    //TODO: Set up event system for initially selected buttons
    void Awake()
    {
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
