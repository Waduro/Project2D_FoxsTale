using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public bool isGem, isFood; // Defines the variables of collectibles

    private bool isCollected; // A bool that indicates the object is collected

    public GameObject collectEffect;

    // Checks if the player passed through the collectible
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !isCollected)
        {
            // Checks is the collectible is a gem
            if (isGem) 
            {
                LevelManager.instance.gemsCollected++; // Give +1 gem

                UIController.Instance.UpdateGemCount();

                Instantiate(collectEffect, transform.position, transform.rotation);

                AudioManager.instance.PlaySoundEffect(6);

                isCollected = true;
                Destroy(gameObject); // Destroy the gem
            }
            else if (isFood) // Checks is the collectible is food
            {
                // Check is the player can get more lives
                if (PlayerHealthController.Instance.currentHealth != PlayerHealthController.Instance.maxHealth)
                {
                    PlayerHealthController.Instance.HealPlayer(); // Heals the player

                    AudioManager.instance.PlaySoundEffect(7);

                    isCollected = true;

                    Destroy(gameObject); // Destroy the food
                }
            }
        }
    }
}
