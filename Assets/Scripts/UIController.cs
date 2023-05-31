using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance; // Makes the C# code aviable for external use

    public Image heart1, heart2, heart3; // Variables of hurts images

    public Sprite heartFull, heartEmpty, heartHalf; // Sprites of the hearts

    public Text gemText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelCompletedText;

    // Makes this C# code for the external use
    private void Awake() 
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();

        FadeFromBlack();
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

    public void UpdateHealthDisplay() 
    {
        // This swicth case uses the health controller sistem and according to the players lives shows the hearts
        switch(PlayerHealthController.Instance.currentHealth) 
        {
            // Full hearts
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;

                break;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;

                break;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;

                break;
            
            // Half hearts
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;

                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;
            
            // No hearts
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;

                break;
        }    
    }
    
    public void UpdateGemCount() 
    {
        gemText.text = LevelManager.instance.gemsCollected.ToString();
    }

    public void FadeToBlack() 
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack() 
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}
