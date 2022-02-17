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
    public string part1;
    public string part2;
    public string part3;
    public string part4;
    public string part5;
    public long lastLoggedIn;
    public long createdOn;
    public long updatedOn;


    public Recipe()
    {
       
    }

    public Recipe(string part1, string part2, string part3, string part4, string part5)
    {
        this.part1 = part1;
        this.part2 = part2;
        this.part3 = part3;
        this.part4 = part4;
        this.part5 = part5;

        var timestamp = this.GetTimeUnix();
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
