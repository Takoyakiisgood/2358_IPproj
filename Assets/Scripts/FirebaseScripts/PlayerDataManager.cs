/******************************************************************************
Team Name: 2359Studios

Author: Jordan Yeo Xiang Yu, Celest Goh Zi Xuan, Theng Sun Yu, Esther Ho Enqi, Ng Hui Ling

Name of Class: PlayerDataManager

Description of Class: This class will get player data and update firebase database.

Date Created: 20/12/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    //All Player Data
    public Text userName;
    public Text email;
    public Text joinedDate;
    public Text lastLoggedIn;
    public Text timePlayed;


    // Firebase Managers
    public FirebaseManager fbMgr;
    public AuthManager auth;

    private string sceneName;
    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// Check current game scene and check if user is logged in.
    /// </summary>
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (auth.GetCurrentUser() != null)
        {
            UpdatePlayerData(auth.GetCurrentUser());
        }
        
        
    }
    /// <summary>
    /// Update user data in the profile page after login
    /// </summary>
    public async void UpdatePlayerData(string uuid)
    {
        PlayerData playerData = await fbMgr.GetPlayerData(uuid);
        PlayerStats playerStats = await fbMgr.GetPlayerStats(uuid);
        if (playerData != null)
        {

            if (sceneName == "MainMenu")
            {
                email.text = playerData.email;
                userName.text = playerData.userName;
                joinedDate.text = playerData.createdOn.ToString();
                lastLoggedIn.text = playerData.lastLoggedIn.ToString();
                timePlayed.text = playerStats.totalTimeSpent.ToString() + " Seconds";
            }
            
            
        }
    }



   
    public void ResetPlayerData()
    {
        UpdatePlayerData(auth.GetCurrentUser());
    }
    public string UnixToDateTime(long timeStamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timeStamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        return dateTime.ToString("dd MMM yyyy");
    }
}
