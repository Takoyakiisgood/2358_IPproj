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
using Proyecto26;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    //All Player Data
    public Text joinedDate;
    public Text lastLoggedIn;
    public Text timePlayed;
    private string databaseURL = "https://mesakanharmoni-default-rtdb.asia-southeast1.firebasedatabase.app/players/";

    public TextMeshProUGUI recipeText;
    private string sceneName;

    Recipe recipe = new Recipe();
    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// Check current game scene and check if user is logged in.
    /// </summary>
    void Start()
    {
        RetrieveFromDatabase();
        
        //Scene currentScene = SceneManager.GetActiveScene();
        //sceneName = currentScene.name;
        //if (auth.GetCurrentUser() != null)
        //{
        //    UpdateRecipe(auth.GetCurrentUser());
        //}


    }

    private void UpdateRecipe()
    {
       //Debug.Log(recipe.part1.ToString());
    }
    /// <summary>
    /// Update user data in the profile page after login
    /// </summary>
    private void RetrieveFromDatabase()
    {
        RestClient.Get<Recipe>(databaseURL + "recipe/chineseCulture/1/instructions" + ".json?auth=").Then(response =>
        {
            recipe = response;
            Debug.Log(recipe.part1);
            UpdateRecipe();
        });
    }




    public string UnixToDateTime(long timeStamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timeStamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        return dateTime.ToString("dd MMM yyyy");
    }
}
