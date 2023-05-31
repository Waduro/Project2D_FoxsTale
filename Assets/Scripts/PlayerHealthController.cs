using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController Instance; // Creates an instance for being used in other C# scripts

    public int currentHealth, maxHealth; // Defines two Healths variables
    public float invincibleLength; // Defines a variable for invincible time where the player cannot receive damage
    private float invincibleCounter; // Define a variable of the amount of invicible time of the player

    private SpriteRenderer spriteRenderer; // Gets the components of the Sprite Renderer

    public GameObject deathEffect;

    private void Awake() // Awakes this C# code for external use
    {
        Instance = this; // Calls this instance
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Defines the current health as max health (When the game starts)

        spriteRenderer = GetComponent<SpriteRenderer>(); // Gets the Sprite Renderer component
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the invincible counter is active and reduce the counting
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime; // Reduce the counter until 0

            // Checks is the player did not recive any damage, define its alpha color as 1
            if (invincibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f); // The players alpha color is 1
            }
        }
    }

    public void DealDamage() // Defines a Method for damage
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--; // reduce the players lives by 1 every time gets damage

            PlayerController.instance.animator.SetTrigger("Hurt"); // Activates the Hurt animation

            AudioManager.instance.PlaySoundEffect(9);

            // Checks if the player lives i minor or equal than 0, in this case distroy the player object
            if (currentHealth <= 0) 
            {
                currentHealth = 0;

                Instantiate(deathEffect, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);

                AudioManager.instance.PlaySoundEffect(8);

                LevelManager.instance.RespwanPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength; // Makes the invincible counter equal to invicible length, reset the timer

                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f); // Checks is the player did recive any damage, define its alpha color as 0.5

                PlayerController.instance.KnockBack(); // Calls the KnockBack Method
            }

            UIController.Instance.UpdateHealthDisplay(); // Calls the UpdateHealthDisplay Method in the UIController C# script
        }
        
    }

    public void HealPlayer() 
    {
        currentHealth++;

        if (currentHealth > maxHealth) 
        { 
            currentHealth = maxHealth;
        }

        UIController.Instance.UpdateHealthDisplay();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
