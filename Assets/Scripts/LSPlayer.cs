using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed = 10.0f;

    public bool levelLoanding;

    public LSManager theManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f) {
                if (currentPoint.rigth != null) {
                    SetNextPoint(currentPoint.rigth);
                }

            }
            if (Input.GetAxisRaw("Horizontal") < -0.5f) {
                if (currentPoint.left != null) {
                    SetNextPoint(currentPoint.left);
                }

            }

            if (Input.GetAxisRaw("Vertical") > 0.5f) {
                if (currentPoint.up != null) {
                    SetNextPoint(currentPoint.up);
                }

            }

            if (Input.GetAxisRaw("Vertical") < -0.5f) {
                if (currentPoint.down != null) {
                    SetNextPoint(currentPoint.down);
                }

            }

            if (currentPoint.isLevel)
            {
                LSUIManager.instance.ShowInfo(currentPoint);

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    levelLoanding = true;

                    theManager.LoadLevel();
                }
            }
        }
    }

    public void SetNextPoint(MapPoint nextPoint) 
    {
        currentPoint = nextPoint;
        LSUIManager.instance.HideInfo();
    }
}
