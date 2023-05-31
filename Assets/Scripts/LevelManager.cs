using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; // Makes this C# code aviable for external use

    public float waitToRespwn; // Variables of time for respawning

    public int gemsCollected; // Quantity of collected gems

    public float timeInLevel;

    public string levelToLoad;

    // Makes this code aviable for external use
    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    // Method of player respawning
    public void RespwanPlayer() 
    {
        StartCoroutine(RespawnCoroutine()); // Uses a coroutine for get seconds before runs the Method
    }

    // Creates a respawn coroutine
    IEnumerator RespawnCoroutine()
    {
        PlayerController.instance.gameObject.SetActive(false); // First destroy the player

        yield return new WaitForSeconds(waitToRespwn - (1.0f / UIController.Instance.fadeSpeed)); // Waits some second according to the waitToRespawn variable

        UIController.Instance.FadeToBlack();

        yield return new WaitForSeconds((1.0f / UIController.Instance.fadeSpeed) + 0.2f);

        UIController.Instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true); // Activate again the player sprite

        PlayerController.instance.transform.position = CheckPointController.instance.spawnPoint; // Takes the player to the last passed checkpoint or to the beginning

        PlayerHealthController.Instance.currentHealth = PlayerHealthController.Instance.maxHealth; // Gives to player the initial lives

        UIController.Instance.UpdateHealthDisplay(); // We call this Method again for a correct function of the UI display of the hearts

    }

    public void EndLevel() 
    {
        StartCoroutine(EndLevelCoroutine());
    }

    public IEnumerator EndLevelCoroutine() 
    {
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.stopInput = true;

        CameraController.Instance.stopFollow = true;

        UIController.Instance.levelCompletedText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.Instance.FadeToBlack();

        yield return new WaitForSeconds((3.0f / UIController.Instance.fadeSpeed) + 1.25f);

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        
        if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }

        if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time") || PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time") == 0)
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }

        SceneManager.LoadScene(levelToLoad);
    }
}
