using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended }; // States Machines

    public bossStates currentstates;

    public Transform theBoss;
    public Animator animator;

    [Header("Stats")]
    public float moveSpeed;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Movemement Points")]
    public Transform leftPoint, rightPoint;

    private bool moveRight;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;

    public GameObject hitBox;

    [Header("Health")]
    public int healthBoss;
    public GameObject hitBossExplotion, winPlatforms;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;


    // Start is called before the first frame update
    void Start()
    {
        currentstates = bossStates.moving;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentstates) 
        {
            case bossStates.shooting:
                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;

                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

                    newBullet.transform.localScale = theBoss.localScale;

                    AudioManager.instance.soundEffects[2].Play();
                }
                break;

            case bossStates.hurt:
                if (hurtCounter > 0) 
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0) 
                    {
                        currentstates = bossStates.moving;

                        mineCounter = 0;

                        if (isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);

                            Instantiate(hitBossExplotion, theBoss.position, theBoss.rotation);

                            winPlatforms.SetActive(true);

                            AudioManager.instance.StopBossMusic();

                            BossDefeated();

                            currentstates = bossStates.ended;

                            //AudioManager.instance.backgroundMusic.Play();
                        }
                    }
                }
                break;

            case bossStates.moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one;

                        moveRight = false;

                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);

                    if (theBoss.position.x < leftPoint.position.x) 
                    {
                        theBoss.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                        moveRight = true;

                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;

                if (mineCounter <= 0) 
                {
                    mineCounter = timeBetweenMines;

                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }

                break;
        }
    }

    public void TakeHit() 
    {
        currentstates = bossStates.hurt;
        hurtCounter = hurtTime;

        animator.SetTrigger("hit");
        AudioManager.instance.soundEffects[0].Play();

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();

        if (mines.Length > 0)
        {
            foreach (BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        healthBoss--;

        if (healthBoss <= 0)
        {
            isDefeated = true;
        }
        else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }

    private void EndMovement() 
    {
        currentstates = bossStates.shooting;

        shotCounter = 0.0f;

        animator.SetTrigger("stopMoving");

        hitBox.SetActive(true);
    }

    public void BossDefeated() {
        StartCoroutine(BossDefeatedCo());
    }

    public IEnumerator BossDefeatedCo() {
        yield return new WaitForSeconds(5.0f);

        AudioManager.instance.backgroundMusic.Play();
    }
}
