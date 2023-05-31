using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager instance;

    public string mainMenuScene;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;

    public Text levelName, gemsFound, gemsTarget, bestTime, targetTime;

    public AudioSource musicLevel;

    public Animator animator;

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        FadeFromBlack();

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1.0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1.0f) {
                shouldFadeToBlack = false;
            }
        } else if (shouldFadeFromBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0.0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0.0f) {
                shouldFadeFromBlack = false;
            }
        }
    }
    public void FadeToBlack() {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack() {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

    public void ShowInfo(MapPoint levelInfo) 
    {
        levelName.text = levelInfo.levelName;

        gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
        gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

        targetTime.text = "TARGET: " + levelInfo.targetTime + "s";

        if (levelInfo.bestTime == 0)
        {
            bestTime.text = "BEST ---";
        }
        else 
        {
            bestTime.text = "BEST: " + levelInfo.bestTime.ToString("F2") + "s";
        }

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo() 
    {
        levelInfoPanel.SetActive(false);
    }

    IEnumerator GoBackToMenu() 
    {
        animator.SetBool("buttonPressed", true);

        FadeToBlack();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(mainMenuScene);
    }

    public void GoBackMenuCoroutine() 
    {
        StartCoroutine(GoBackToMenu());
    }
}
