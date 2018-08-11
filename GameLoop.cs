using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public Transform[] boardTile;
    public Transform[] edgeTile;

    public int rows, columns;

    private GameObject gameBoard;

    private void Start()
    {
        //edgeTile = new Transform[4];
        gameBoard = new GameObject("Game Board");

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (j == 0)
                    Instantiate(edgeTile[0], new Vector2(j, i), Quaternion.identity, gameBoard.transform);
                else if (j == rows - 1)
                    Instantiate(edgeTile[1], new Vector2(j, i), Quaternion.identity, gameBoard.transform);
                else if (i == 0)
                    Instantiate(edgeTile[2], new Vector2(j, i), Quaternion.identity, gameBoard.transform);
                else if (i == columns - 1)
                    Instantiate(edgeTile[3], new Vector2(j, i), Quaternion.identity, gameBoard.transform);
                else
                    Instantiate(boardTile[Random.Range(0,3)], new Vector2(j, i), Quaternion.identity, gameBoard.transform);
            }
        }


    }

}