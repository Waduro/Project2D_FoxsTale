using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance; // Makes this C# code aviable for external use

    public CheckPoint[] checkpoints; // Creates an arrays of checkpoint type

    public Vector3 spawnPoint;

    // Makes this code aviable for external use
    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpoints = GetComponentsInChildren<CheckPoint>(); // Gets the checkpoints from the childrens

        spawnPoint = PlayerController.instance.transform.position;
    }

    // This Method deactivate all the checkpoints transversing the array
    public void DeactivateCheckpoints() 
    {
        for (int i = 0; i < checkpoints.Length; i++) 
        {
            checkpoints[i].ResetCheckpoint(); // Turn into the off checkpoint sprite
        }
    }

    // Takes
    public void SetSpawnPoint(Vector3 newSpanwPoint) 
    {
        spawnPoint = newSpanwPoint;
    }
}
