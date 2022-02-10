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

public class Recipe
{
    public string instructions;
    public string cultural;
    public string food;
    public long lastLoggedIn;
    public long createdOn;
    public long updatedOn;


    public Recipe()
    {
       
    }

    public Recipe(string instructions, string cultural, string food)
    {
        this.instructions = instructions;
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
}
