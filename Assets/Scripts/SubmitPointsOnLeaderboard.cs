using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using PlayFab;
using PlayFab.ClientModels;

public class SubmitPointsOnLeaderboard : MonoBehaviour
{
    public static SubmitPointsOnLeaderboard Instance;

    public float timeInLevel;
    public int timeInLevelIntValue;
    

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel = TimerInSpeedrun.instance.timeInLevel;
        timeInLevelIntValue = Convert.ToInt32(timeInLevel * 100);
    }

    public void SendSpeedrunScore() {
        PlayfabManager.Instance.SendLeaderboard(timeInLevelIntValue);
    }
}
