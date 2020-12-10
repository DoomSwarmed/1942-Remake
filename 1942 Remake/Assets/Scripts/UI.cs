using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Player playerClass;

    public GameObject livesParent;

    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playerClass.maxHealth; i++)
        {
            if (playerClass.health < i + 1)
                livesParent.transform.GetChild(i).gameObject.SetActive(false);
            else
                livesParent.transform.GetChild(i).gameObject.SetActive(true);
        }

        scoreText.text = "Score\n" + playerClass.score.ToString("000000");
    }
}
