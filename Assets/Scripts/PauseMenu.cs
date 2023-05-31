using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public string levelSelect, mainMenu;
    public GameObject pauseScreen;
    public bool isPaused;

    private void Awake() 
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause() 
    {
        if (isPaused) 
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else 
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void LevelSelect() 
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1.0f;
    }

    public void MainMenus() {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1.0f;
    }
}
