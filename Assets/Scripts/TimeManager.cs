using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeDisplay;

    private float time = 0;
    private float sec;
    private float min;

    public static TimeManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator StopWatch()
    {
        while (true)
        {
            time += Time.deltaTime;
            DisplayTime(time);
            yield return null;
        }
    }

    void DisplayTime(float time)
    {
        float min = Mathf.FloorToInt(time / 60);
        float sec = Mathf.FloorToInt(time % 60);

        timeDisplay.text = "TIME: " + string.Format("{0:00}:{1:00}", min, sec);
    }

    public void StartTimer()
    {
        StartCoroutine("StopWatch");
    }

    public void StopTimer()
    {
        StopCoroutine("StopWatch");
    }

    public float GetCurrentSec()
    {
        StopTimer();
        float RoundTime = Mathf.CeilToInt(time);
        Debug.Log(RoundTime);
        return RoundTime;     
    }
    
}
