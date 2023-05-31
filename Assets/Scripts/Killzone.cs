using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    // Checks if the player fell in the killzone
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            LevelManager.instance.RespwanPlayer(); // Respawm the player in the las checkpoint
        }
    }
}
