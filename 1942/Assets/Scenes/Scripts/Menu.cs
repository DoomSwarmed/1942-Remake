using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    RectTransform playButton;
    RectTransform settingsButton;
    public GameObject options;
    public GameObject mainMenu;
    public AudioClip buttonPress;
    public AudioSource spagetti;
    public AudioMixer audioMixer;

    void Start()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        spagetti.PlayOneShot(buttonPress);
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void MenuMain()
    {
        spagetti.PlayOneShot(buttonPress);
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitted");
    }

    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("master", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("music", volume);
        
    }
}
