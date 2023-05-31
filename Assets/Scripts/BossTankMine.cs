using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explotion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Destroy(gameObject);

            Instantiate(explotion, transform.position, transform.rotation);

            PlayerHealthController.Instance.DealDamage();
        }
    }

    public void Explode() 
    {
        Destroy(gameObject);

        Instantiate(explotion, transform.position, transform.rotation);
    }
}
