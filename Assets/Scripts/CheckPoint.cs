using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Gets the sprite renderer component

    public Sprite checkPoinOn, checkPoinOff; // Gets the sprites

    // First deactivate all the checkpoints and then activate de checkpoint wheres the player is
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            CheckPointController.instance.DeactivateCheckpoints();

            spriteRenderer.sprite = checkPoinOn;

            CheckPointController.instance.SetSpawnPoint(transform.position);
        }
    }

    // Reset the checkpoint and convert it into the off sprite
    public void ResetCheckpoint() 
    {
        spriteRenderer.sprite = checkPoinOff;
    }
}
