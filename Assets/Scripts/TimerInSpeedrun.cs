using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TimerInSpeedrun : MonoBehaviour
{
    public static TimerInSpeedrun instance;

    public float timeInLevel;
    public TMP_Text counter;
    public bool timerActivated;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActivated == true) {
            GetTimeInLevel();
            counter.text = timeInLevel.ToString("f2") + "s";
        }
    }

    public void GetTimeInLevel() {
        timeInLevel += Time.deltaTime;
    }


}
