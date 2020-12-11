using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    RectTransform playButton;
    RectTransform settingsButton;

    void Start()
    {
        playButton = transform.Find("Play").GetComponent<RectTransform>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        print("There are no settings");
    }
}
