using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    public GameObject[] enemies;
 

    public float timeToNextSpawn;
    public float timer;
    private GameObject playerCharacter;

    private void Awake()
    {
        timeToNextSpawn = Random.Range(3, 15);
    }

    private void Update()
    {
        playerCharacter = GameObject.Find("Player");
        
        timer += 1.0f * Time.deltaTime;

        if(timer >= timeToNextSpawn && playerCharacter.GetComponent<PlayerCharacter>().gameWon == false && playerCharacter.GetComponent<PlayerCharacter>().health > 0)
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