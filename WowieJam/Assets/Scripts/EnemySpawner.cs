using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3[] spawnPoints; //Assigned in editor
    public GameObject monster;

    float timer = 0.0f;
    float spawnTime = 2.0f;
    float totalTime = 0.0f;

    List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        timer = 4.0f;

        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime = 2.0f - (0.2f * totalTime) / (1.0f + 0.1f * totalTime);

        timer -= Time.deltaTime;
        totalTime += Time.deltaTime;

        if(timer <= 0.0f)
        {
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        timer = spawnTime;
        int pointIndex = Random.Range(0, spawnPoints.Length);
        GameObject temp = Instantiate(monster, spawnPoints[pointIndex], Quaternion.identity);
        enemies.Add(temp);
    }

    void OnDisable()
    {
        foreach(GameObject enemy in enemies)
        {
            try
            {
                Destroy(enemy);
            }catch(System.Exception e)
            {
                //This is a quick hack to get it working in deadline
                //TODO: Fix this
            }
        }
    }
}
