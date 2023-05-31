using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private Animator animator;

    public float bounceForce;

    public AudioSource bounceSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.rb.velocity = new Vector2(PlayerController.instance.rb.velocity.x, bounceForce);

            animator.SetTrigger("bounce");

            bounceSound.Play();
        }
    }
}
