using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string startScene, mainMenuScene, speedrunScene;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public AudioSource musicLevel;

    public Animator animator;

    void Start() 
    {
        FadeFromBlack();

        animator = GetComponentInChildren<Animator>();
    }

    void Update() 
    {
        if (shouldFadeToBlack) 
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1.0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1.0f) 
            {
                shouldFadeToBlack = false;
            }
        } 
        else if (shouldFadeFromBlack) 
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0.0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0.0f) 
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void StartGameCoroutine() 
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame() 
    {
        animator.SetBool("buttonPressed", true);

        FadeToBlack();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(startScene);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void DeletesUserData() 
    {
        PlayerPrefs.DeleteAll();
    }

    public void GoBackMainMenu() 
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void FadeFromBlack() 
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

    public void FadeToBlack() 
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void StartSpeedrunCoroutine() {
        StartCoroutine(StartSpeedrun());
    }

    IEnumerator StartSpeedrun() {
        animator.SetBool("buttonPressed", true);

        FadeToBlack();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(speedrunScene);
    }
}
