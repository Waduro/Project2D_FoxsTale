using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) // Checks the players collision
    {
        if (other.tag == "Player") // Detects the tag Player
        {
            PlayerHealthController.Instance.DealDamage(); // Calls the DealDamage Method in PlayerHealthController
        }
    }
}
