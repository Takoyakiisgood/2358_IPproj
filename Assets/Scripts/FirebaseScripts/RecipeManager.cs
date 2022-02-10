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

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    //All Player Data
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
            UpdateRecipe(auth.GetCurrentUser());
        }
        
        
    }
    /// <summary>
    /// Update user data in the profile page after login
    /// </summary>
    public async void UpdateRecipe(string uuid)
    {
        Recipe recipe = await fbMgr.GetRecipe(uuid);
        if (recipe != null)
        {

            if (sceneName == "Main")
            {
                //joinedDate.text = recipe.createdOn.ToString();
                //lastLoggedIn.text = recipe.lastLoggedIn.ToString();
            }
            
            
        }
    }



  
    public string UnixToDateTime(long timeStamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timeStamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        return dateTime.ToString("dd MMM yyyy");
    }
}
