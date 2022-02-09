/******************************************************************************
Team Name: 2359Studios

Author: Jordan Yeo Xiang Yu, Celest Goh Zi Xuan, Theng Sun Yu, Esther Ho Enqi, Ng Hui Ling

Name of Class: PlayerStats

Description of Class: This class wll handle player stats and update data to firebase.

Date Created: 18/12/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats
{
    public string userName;
    public long createdOn;
    public long updatedOn;
    public long lastLoggedIn;
    public int totalMistakesMade;
    public float totalTimeSpent;
    public float chineseCultureTimeTaken;
    public float indianCultureTimeTaken;
    public float malayCultureTimeTaken;
    public float eurasianCultureTimeTaken;
    public PlayerStats()
    {
       
    }

    public PlayerStats(string userName, int totalMistakesMade, float totalTimeSpent, float chineseCultureTimeTaken, float indianCultureTimeTaken, float malayCultureTimeTaken, float eurasianCultureTimeTaken)
    {
        this.userName = userName;
        this.totalMistakesMade = totalMistakesMade;
        this.totalTimeSpent = totalTimeSpent;
        this.chineseCultureTimeTaken = chineseCultureTimeTaken;
        this.indianCultureTimeTaken = indianCultureTimeTaken;
        this.malayCultureTimeTaken = malayCultureTimeTaken;
        this.eurasianCultureTimeTaken = eurasianCultureTimeTaken;

        long timestamp = this.GetTimeUnix();
        this.updatedOn = timestamp;
        this.createdOn = timestamp;
        this.lastLoggedIn = timestamp;
    }

    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }
}
