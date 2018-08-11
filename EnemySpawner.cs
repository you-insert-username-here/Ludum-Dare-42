using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    public GameObject[] enemies;
 

    public float timeToNextSpawn;
    public float timer;

    private void Awake()
    {
        timeToNextSpawn = Random.Range(3, 15);
    }

    private void Update()
    {
        timer += 1.0f * Time.deltaTime;

        if(timer >= timeToNextSpawn)
        {
            SpawnEnemy(enemies[Random.Range(0, enemies.Length)]);
            timer = 0.0f;
            timeToNextSpawn = Random.Range(3, 15);
        }
    }

    public void SpawnEnemy(GameObject enemySpawn)
    {
        Instantiate(enemySpawn, this.transform.position, Quaternion.identity);
    }

}