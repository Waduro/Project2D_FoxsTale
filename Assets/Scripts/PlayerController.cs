using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; // Makes this C# code aviable for external use


    // Headers are for more organized variables
    [Header("Stats")]
    public float moveSpeed; // Define the velocity of the player movement
    public float jumpForce; // Define the jump force
    public float bounceForce;
    private bool canDoubleJump; // A variable that permit double jump
    public float knockBackLength, knockBackForce; // Duration and force of the Knockback of the player
    private float knockBackCounter; // Time of knocking

    [Header("Componets")]
    public Rigidbody2D rb; // Gets the componet RigidBody 2D from the player
    public Animator animator; // Creates an animator variable linked with the animator component
    private SpriteRenderer spriteRenderer; // Gets the components of the Sprite Renderer

    [Header("Checkers")]
    private bool isGrounded, isGroundedOnPlatform;
    public Transform groundCheck; // Checks if the player is on the ground
    public LayerMask ground, platform; // Selects the layers of the ground

    public bool stopInput;

    private void Awake()
    {
        instance = this; // Makes aviable this C# code for external use
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Gets the Animator component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Gets the Sprite Renderer component
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            // Checks if the player is not knocked for moving
            if (knockBackCounter <= 0) 
            {
                // Checks if the player is on the ground for double jumping
                if (isGrounded == true || isGroundedOnPlatform == true) 
                {
                    canDoubleJump = true;
                }

                rb.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rb.velocity.y); // Player movement

                isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground); // Checks the player on the ground using a circle
                isGroundedOnPlatform = Physics2D.OverlapCircle(groundCheck.position, 0.2f, platform);

                // Detect if the player press the Space key and it is on the ground for jumping
                if (Input.GetButtonDown("Jump") && isGrounded == true) 
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Player jump
                    AudioManager.instance.PlaySoundEffect(10);
                } 
                else 
                {
                    if (canDoubleJump && Input.GetButtonDown("Jump")) // Makes the players double jump
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Player jump
                        AudioManager.instance.PlaySoundEffect(10);
                        canDoubleJump = false;
                    }
                }

                // Checks is the player is walking backwards or forwards
                if (rb.velocity.x < 0) 
                {
                    spriteRenderer.flipX = true; // Flips the Sprite to its normal position
                } 
                else if (rb.velocity.x > 0) 
                {
                    spriteRenderer.flipX = false; // Flips the sprite to left
                }
            } 
            else // If the player gets knocked
            {
                knockBackCounter -= Time.deltaTime; // Reduce the time of the counter

                // Applies the knock forces in its respectives x axis
                if (!spriteRenderer.flipX) 
                {
                    rb.velocity = new Vector2(-knockBackForce, rb.velocity.y);
                } 
                else 
                {
                    rb.velocity = new Vector2(knockBackForce, rb.velocity.y);
                }
            }
        }

        animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x)); // Checks if the player is moving to reproduce the animation
        animator.SetBool("isGrounded", isGrounded); // Checks if the player is not on the ground to reproduce the falling/jumping animation
    }

    public void KnockBack() // The method of the KnockBAck player
    {
        knockBackCounter = knockBackLength; // Define the counter as the knockback length
        rb.velocity = new Vector2(0.0f, knockBackForce); // Makes the knockback animation
    }

    public void Bounce() 
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }
}
