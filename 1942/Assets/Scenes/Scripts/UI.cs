using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Player playerClass;

    public GameObject gameOverScreen;
    public GameObject livesParent;
    public GameObject spawner;
    public GameObject pause;

    public Text scoreText;
    public Text gameOverScore;
    public Text gameOver;
    public Text playAgain;
    public Text highScore;
    public Text currentScore;
    public Text currentScoreText;

    public AudioMixer mix;
    void Start()
    {
        

        gameOverScore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerClass.enabled == false)
            StartCoroutine(player());
        if (Input.GetKeyDown("space") && gameOverScreen.active == true)
            StartGame();

        for (int i = 0; i < playerClass.maxHealth; i++)
        {
            if (playerClass.health < i + 1)
                livesParent.transform.GetChild(i).gameObject.SetActive(false);
            else
                livesParent.transform.GetChild(i).gameObject.SetActive(true);
        }

        scoreText.text = "Score\n" + playerClass.score.ToString();
        
    }

    public void EndGame()
    {
        if (playerClass.score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", playerClass.highScore);
            gameOverScore.text = playerClass.highScore.ToString();
        }
        highScore.text = "High score:";
        currentScore.text = playerClass.compScore.ToString();
        currentScoreText.text = "Your Score:";
        gameOver.text = "game over";
        playAgain.text = "space to play again";
        gameOverScreen.SetActive(true);
        spawner.SetActive(false);
    }

    void StartGame()
    {
        gameOverScreen.SetActive(false);
        playerClass.EndGame();
        StartCoroutine(wait());
        spawner.SetActive(true);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("Highscore");
        gameOverScore.text = "0";
    }

    public void Pause()
    {
        mix.SetFloat("gameSFX", -80);
        pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        mix.SetFloat("gameSFX", -40);
    }



    public void Menu()
    {
        mix.SetFloat("gameSFX", -40);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    IEnumerator player()
    {
        yield return new WaitForSeconds(1);
        playerClass.enabled = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }
}
