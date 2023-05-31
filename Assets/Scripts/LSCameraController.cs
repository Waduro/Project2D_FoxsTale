using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{
    public Vector2 minPosition, maxPosition;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xPosition = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
        float yPosition = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);

        transform.position =  new Vector3 (xPosition, yPosition, transform.position.z);
    }
}
