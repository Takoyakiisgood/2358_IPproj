/******************************************************************************
Team Name: 2359Studios

Author: Jordan Yeo Xiang Yu, Celest Goh Zi Xuan, Theng Sun Yu, Esther Ho Enqi, Ng Hui Ling

Name of Class: PlayerData

Description of Class: This class wll handle playerdata and update data to firebase.

Date Created: 15/12/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData
{
    public string userName;
    public string email;
    public string cultural;
    public string food;
    public long lastLoggedIn;
    public long createdOn;
    public long updatedOn;
    public int currentStep;
    public bool active;


    public PlayerData()
    {
       
    }

    public PlayerData(string userName, string email, string cultural, string food, int currentStep, bool active)
    {
        this.userName = userName;
        this.email = email;
        this.currentStep = currentStep;
        this.active = active;
        this.cultural = cultural;
        this.food = food;
        var timestamp = this.GetTimeUnix();
        this.lastLoggedIn = timestamp;
        this.createdOn = timestamp;
        this.updatedOn = timestamp;
    }

    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }
    public string PrintPlayerData()
    {
        return String.Format("User Name: {0}\n Email: {1}",
            this.userName, this.email
            );
    }
}
