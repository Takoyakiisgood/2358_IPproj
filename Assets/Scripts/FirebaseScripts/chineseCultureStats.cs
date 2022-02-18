using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chineseCultureStats : MonoBehaviour
{

    public bool completed;
    public int mistakesMade;
    public float totalTimeTaken;


    public long createdOn;
    public long updatedOn;

    public string username;
    public chineseCultureStats()
    {

    }
    public chineseCultureStats(bool completed, int mistakesMade, long createdOn, long updatedOn, float totalTimeTaken)
    {
        this.completed = completed;
        this.mistakesMade = mistakesMade;
        this.totalTimeTaken = totalTimeTaken;
        this.createdOn = createdOn;
        this.updatedOn = updatedOn;
    }
    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }
}
