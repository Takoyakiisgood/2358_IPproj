using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float chineseCultureTimeTaken;
    public float eurasianCultureTimeTaken;
    public float indianCultureTimeTaken;
    public float malayCultureTimeTaken;

    public int dishesCompletedChinese;
    public int dishesCompletedEurasian;
    public int dishesCompletedIndian;
    public int dishesCompletedMalay;
    public int totalMistakesMade;

    public long createdOn;
    public long updatedOn;

    public string username;
    public playerStats()
    {

    }
    public playerStats(float chineseCultureTimeTaken, float eurasianCultureTimeTaken, float indianCultureTimeTaken,
        float malayCultureTimeTaken, int dishesCompletedChinese, int dishesCompletedEurasian, int dishesCompletedIndian,
        int dishesCompletedMalay, int totalMistakesMade,long createdOn, long updatedOn, string username)
    {
        this.chineseCultureTimeTaken = chineseCultureTimeTaken;
        this.eurasianCultureTimeTaken = eurasianCultureTimeTaken;
        this.indianCultureTimeTaken = indianCultureTimeTaken;
        this.malayCultureTimeTaken = malayCultureTimeTaken;
        this.dishesCompletedChinese = dishesCompletedChinese;
        this.dishesCompletedEurasian = dishesCompletedEurasian;
        this.dishesCompletedIndian = dishesCompletedIndian;
        this.dishesCompletedMalay = dishesCompletedMalay;
        this.totalMistakesMade = totalMistakesMade;
        this.createdOn = createdOn;
        this.updatedOn = updatedOn;
        this.username = username;
    }                 
    public long GetTimeUnix()
    {                  
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }                  
}                      
                       



                       
                       
                       

                       