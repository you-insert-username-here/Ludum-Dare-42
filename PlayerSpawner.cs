using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour 
{    
    public GameObject player;

    public void SpawnPlayer(int x, int y)
    {
        int randomX = Random.Range(1, x - 1);
        int randomY = Random.Range(1, y - 1);

        Instantiate(player, new Vector3(randomX, randomY, 0), Quaternion.identity);
    }
}