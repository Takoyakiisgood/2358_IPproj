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


    private string databaseURL = "https://mesakanharmoni-default-rtdb.asia-southeast1.firebasedatabase.app/";




    Dictionary<string, object> step1 = new Dictionary<string, object>();

    private void Awake()
    {
        Instance = this;
        RetrieveFromDatabase();
    }
    /// <summary>
    /// Check current game scene and check if user is logged in.
    /// </summary>
    void Start()
    {
        
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
        RestClient.Get<Recipe>(databaseURL + "recipe/chineseCulture/1/instructions/step1.json").Then(response =>
        {

            Recipe recipe = response;
            GameManager.instance.step1List.Add(recipe.part1);
            GameManager.instance.step1List.Add(recipe.part2);
            GameManager.instance.step1List.Add(recipe.part3);
        });
        RestClient.Get<Recipe>(databaseURL + "recipe/chineseCulture/1/instructions/step2.json").Then(response =>
        {

            Recipe recipe = response;
            GameManager.instance.step2List.Add(recipe.part1);
            GameManager.instance.step2List.Add(recipe.part2);
            GameManager.instance.step2List.Add(recipe.part3);
            GameManager.instance.step2List.Add(recipe.part4);
            GameManager.instance.step2List.Add(recipe.part5);
        });
        RestClient.Get<Recipe>(databaseURL + "recipe/chineseCulture/1/instructions/step3.json").Then(response =>
        {

            Recipe recipe = response;
            GameManager.instance.step3List.Add(recipe.part1);
        });
        RestClient.Get<Recipe>(databaseURL + "recipe/chineseCulture/1/instructions/step4.json").Then(response =>
        {

            Recipe recipe = response;
            GameManager.instance.step4List.Add(recipe.part1);
            GameManager.instance.step4List.Add(recipe.part2);
            GameManager.instance.step4List.Add(recipe.part3);
        });
        RestClient.Get<Recipe>(databaseURL + "recipe/chineseCulture/1/instructions/step5.json").Then(response =>
        {

            Recipe recipe = response;
            GameManager.instance.step5List.Add(recipe.part1);
            GameManager.instance.step5List.Add(recipe.part2);
        });
    }




    public string UnixToDateTime(long timeStamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timeStamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        return dateTime.ToString("dd MMM yyyy");
    }
}
