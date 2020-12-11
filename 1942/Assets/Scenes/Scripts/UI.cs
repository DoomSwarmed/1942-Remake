using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Player playerClass;

    public GameObject gameOverScreen;
    public GameObject livesParent;

    public Text scoreText;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameOverScreen.active == true)
            StartGame();

        for (int i = 0; i < playerClass.maxHealth; i++)
        {
            if (playerClass.health < i + 1)
                livesParent.transform.GetChild(i).gameObject.SetActive(false);
            else
                livesParent.transform.GetChild(i).gameObject.SetActive(true);
        }

        scoreText.text = "Score\n" + playerClass.score.ToString("000000");
    }

    public void EndGame()
    {
        gameOverScreen.SetActive(true);
    }

    void StartGame()
    {
        gameOverScreen.SetActive(false);
        playerClass.EndGame();
    }
}
