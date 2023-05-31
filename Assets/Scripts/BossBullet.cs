using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-bulletSpeed * transform.localScale.x * Time.deltaTime, 0.0f, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.Instance.DealDamage();
        }

        Destroy(gameObject);
    }
}
