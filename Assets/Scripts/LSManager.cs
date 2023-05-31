using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    public LSPlayer thePlayer;

    private MapPoint[] allPoints;

    // Start is called before the first frame update
    void Start()
    {
        allPoints = GetComponentsInChildren<MapPoint>();

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach (MapPoint point in allPoints)
            {
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.currentPoint = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel() 
    {
        StartCoroutine(LoadLevelCoroutine());
    }

    public IEnumerator LoadLevelCoroutine() 
    {
        LSUIManager.instance.FadeToBlack();

        LSUIManager.instance.animator.SetBool("buttonPressed", true);

        yield return new WaitForSeconds((1.0f / LSUIManager.instance.fadeSpeed) + 0.25f);

        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
