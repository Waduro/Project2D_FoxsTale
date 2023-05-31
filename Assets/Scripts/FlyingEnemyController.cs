using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public Transform[] points;

    public float moveSpeed;

    public int currentPoint;

    public SpriteRenderer spriteRenderer;

    public float distanceToAttackPlayer, chaseSpeed;

    private Vector3 attackTarget;

    public float cooldownAttack;

    private float attackCounter;

    // Start is called before the first frame update
    void Start() 
    {
        for (int i = 0; i < points.Length; i++) 
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update() 
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer) 
            {
                attackTarget = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.05f) 
                {
                    currentPoint++;

                    if (currentPoint >= points.Length) 
                    {
                        currentPoint = 0;
                    }
                }

                if (transform.position.x < points[currentPoint].position.x) 
                {
                    spriteRenderer.flipX = true;
                } 
                else if (transform.position.x > points[currentPoint].position.x) 
                {
                    spriteRenderer.flipX = false;
                }
            } 
            else 
            {
                if (attackTarget == Vector3.zero) 
                {
                    attackTarget = PlayerController.instance.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, attackTarget) <= 0.1f) 
                {                   
                    attackCounter = cooldownAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }       
    }
}
