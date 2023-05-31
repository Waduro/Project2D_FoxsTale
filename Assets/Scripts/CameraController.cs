using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Transform target; // Gets the object it follows (the camera)
    public Transform farBackground, middleBackground; // Gets the Backgrounds
    public float minHeight, maxHeight;

    private Vector2 lastPosition; // Defines the last position of the Backgrounds

    public bool stopFollow;

    private void Awake() 
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position; // defines the x position equals to the current x position
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow) 
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z); //Takes the target x, y and z position to be followed, using the Clamp function we define the two max values where the camera can move

            Vector2 amountToMove = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);

            farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0.0f); // Defines the far Background postion
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0.0f) * 0.5f; // Defines the middle Backgrounds postions suming it, also it multplies 0.5 times to make the middle bg faster

            lastPosition = transform.position; // Gets the last position again
        }
    }
}
