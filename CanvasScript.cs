using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public Image[] heartTiles;
    public Sprite redHeart;
    public Sprite stoneHeart;
    public Image deathScreen;
    public Image endingScreen;
    public Text endingText;
    public Button titleScreenButton;

    public int health, maxHealth;


    GameObject playerCharacter;    

    private void Awake()
    {
        deathScreen.enabled = false;
        endingScreen.enabled = false;
        endingText.enabled = false;
        //titleScreenButton.enabled = false;
        titleScreenButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerCharacter == null)
            playerCharacter = GameObject.Find("Player");
        else
        {
            health = playerCharacter.GetComponent<PlayerCharacter>().health;
            maxHealth = playerCharacter.GetComponent<PlayerCharacter>().maxHealth;
        }

        EndingScreen();

        if (playerCharacter.GetComponent<PlayerCharacter>().deathTimer >= 2.0f)
        {
            deathScreen.enabled = true;
            endingText.enabled = true;
            titleScreenButton.gameObject.SetActive(true);
        }

        for (int i = 0; i < heartTiles.Length; i++)
        {
            if (i < health)
            {
                heartTiles[i].sprite = redHeart;
            }
            else
            {
                heartTiles[i].sprite = stoneHeart;
            }

            if (i < maxHealth)
            {
                heartTiles[i].enabled = true;
            }
            else
            {
                heartTiles[i].enabled = false;
            }
        }
    }

    void EndingScreen()
    {
        if (playerCharacter.GetComponent<PlayerCharacter>().gameWon == true)
        {
            endingScreen.enabled = true;
            endingText.enabled = true;
            //titleScreenButton.enabled = true;
            titleScreenButton.gameObject.SetActive(true);
        }        
    }

    public void ReturnTitleScreen()
    {
        SceneManager.LoadScene("Title");
    }
}