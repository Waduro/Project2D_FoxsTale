using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public static MapPoint Instance;

    public MapPoint up, rigth, down, left;
    public bool isLevel, isLocked;

    public string levelToLoad, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBagde;

    private void Awake() 
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
        {
            gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
        }

        if (PlayerPrefs.HasKey(levelToLoad + "_time")) 
        {
            bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
        }

        if (gemsCollected >= totalGems && totalGems != 0)
        {
            gemBadge.SetActive(true);
        }

        if (bestTime <= targetTime && bestTime != 0) 
        {
            timeBagde.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
