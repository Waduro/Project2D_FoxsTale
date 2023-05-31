using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            AudioManager.instance.PlayBossMusic();

            gameObject.SetActive(false);
        }
    }
}
