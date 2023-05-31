using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelVictoryUI : MonoBehaviour
{
    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public string mainMenuLevel;

    public AudioSource musicLevel;

    public Animator animator;

    public static LevelVictoryUI instance;

    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
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

    IEnumerator GoBackToMenu() 
    {
        animator.SetBool("buttonPressed", true);

        FadeToBlack();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(mainMenuLevel);
    }

    public void GoBackMenuCoroutine() 
    {
        StartCoroutine(GoBackToMenu());
    }
 }
