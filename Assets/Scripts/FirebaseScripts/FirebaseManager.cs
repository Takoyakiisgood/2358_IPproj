/******************************************************************************
Team Name: 2359Studios

Author: Jordan Yeo Xiang Yu, Celest Goh Zi Xuan, Theng Sun Yu, Esther Ho Enqi, Ng Hui Ling

Name of Class: FirebaseManager

Description of Class: This class will get data and update firebase database.

Date Created: 22/12/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    DatabaseReference dbPlayerStatsReference;
    DatabaseReference dbQuizStatsReference;
    DatabaseReference dbPlayerDataReference;
    DatabaseReference dbZooKeeperStatsReference;

    private string sceneName;
    public void Awake()
    {
        InitializedFirebase();

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    public void InitializedFirebase()
    {
        dbPlayerDataReference = FirebaseDatabase.DefaultInstance.GetReference("players");
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerStats");
    }

    public void UpdatePlayerStats(string uuid, string displayName, int totalMistakesMade, float totalTimeSpent, float chineseCultureTimeTaken, float indianCultureTimeTaken, float malayCultureTimeTaken, float eurasianCultureTimeTaken)
    {
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        playerQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("There was an error creating entries, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;
                if (playerStats.Exists)
                {


                    //Update  
                    PlayerStats sp = JsonUtility.FromJson<PlayerStats>(playerStats.GetRawJsonValue());
                    sp.totalMistakesMade += totalMistakesMade;
                    sp.updatedOn = sp.GetTimeUnix();
                    sp.totalTimeSpent += totalTimeSpent;
                    dbPlayerStatsReference.Child(uuid).Child("totalTimeTaken").SetValueAsync(sp.totalTimeSpent);
                    dbPlayerStatsReference.Child(uuid).Child("updatedOn").SetValueAsync(sp.updatedOn);
                    /**
                    Debug.Log(numFailed);
                    if (sceneName == "DAY1_GameScene")
                    {
                        if(Day1Manager.Instance.Chance != 0)
                        {
                            dbPlayerStatsReference.Child(uuid).Child("day1TimeTaken").SetValueAsync(day1TimeTaken);
                        }
                        else if (Day1Manager.Instance.Chance == 0)
                        {
                            dbPlayerStatsReference.Child(uuid).Child("numFailed").SetValueAsync(sp.numFailed);
                            SceneManager.LoadSceneAsync(sceneName);
                        }
                    }
                    else if(sceneName == "DAY2_GameScene")
                    {
                        if(Day2Manager.Instance.Chance != 0)
                        {
                            dbPlayerStatsReference.Child(uuid).Child("day2TimeTaken").SetValueAsync(day2TimeTaken);
                        }
                        else if (Day2Manager.Instance.Chance == 0)
                        {
                            dbPlayerStatsReference.Child(uuid).Child("numFailed").SetValueAsync(sp.numFailed);
                            SceneManager.LoadSceneAsync(sceneName);
                        }
                    }
                    else if (sceneName == "DAY3_GameScene")
                    {
                        dbPlayerStatsReference.Child(uuid).Child("day3TimeTaken").SetValueAsync(day3TimeTaken);
                    }

                    **/
                }
                else
                {
                    //Create
                    PlayerStats sp = new PlayerStats(displayName, totalMistakesMade, totalTimeSpent, chineseCultureTimeTaken, indianCultureTimeTaken, malayCultureTimeTaken, eurasianCultureTimeTaken);

                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SaveToJson());


                }
            }
        });
    }

    public async Task<Recipe> GetRecipe(string uuid)
    {
        Query q = dbPlayerDataReference.Child(uuid).LimitToFirst(1);
        Recipe recipe = null;

        await dbPlayerDataReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("There was an error retrieving player stats.\nError: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;

                if (ds.Child(uuid).Exists)
                {
                    recipe = JsonUtility.FromJson<Recipe>(ds.Child(uuid).GetRawJsonValue());
                }
            }
        });

        return recipe;
    }
    public async Task<PlayerData> GetPlayerData(string uuid)
    {
        Query q = dbPlayerDataReference.Child(uuid).LimitToFirst(1);
        PlayerData playerData= null;

        await dbPlayerDataReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("There was an error retrieving player stats.\nError: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;

                if (ds.Child(uuid).Exists)
                {
                    playerData = JsonUtility.FromJson<PlayerData>(ds.Child(uuid).GetRawJsonValue());
                    /**
                    if (sceneName == "DAY1_GameScene")
                    {
                        dbPlayerDataReference.Child(uuid).Child("currentDay").SetValueAsync(1);
                    }
                    else if (sceneName == "DAY2_GameScene")
                    {
                        dbPlayerDataReference.Child(uuid).Child("currentDay").SetValueAsync(2);
                    }
                    else if (sceneName == "DAY3_GameScene")
                    {
                        if (QuizManager.Instance.isZooKeeper == true)
                        {
                            dbPlayerDataReference.Child(uuid).Child("currentDay").SetValueAsync(1);
                        }
                        else
                        {
                            dbPlayerDataReference.Child(uuid).Child("currentDay").SetValueAsync(3);
                        }
                        
                    }

                    Debug.Log("DataSnapShot: " + ds.GetRawJsonValue());
                    Debug.Log("Player Data values: " + playerData.SaveToJson());
                    **/
                }

            }

        });

        return playerData;
    }

    public void UpdateZooKeeperStats(string uuid, bool isZooKeeper)
    {
        dbZooKeeperStatsReference.Child(uuid).SetValueAsync(isZooKeeper);
    }

    public async Task<PlayerStats> GetPlayerStats(string uuid)
    {
        PlayerStats playerStats = null;

        await dbPlayerStatsReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("There was an error retrieving player stats.\nError: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;

                if (ds.Child(uuid).Exists)
                {
                    playerStats = JsonUtility.FromJson<PlayerStats>(ds.Child(uuid).GetRawJsonValue());

                    Debug.Log("DataSnapShot: " + ds.GetRawJsonValue());
                    Debug.Log("Player Data values: " + playerStats.SaveToJson());

                }

            }

        });

        return playerStats;
    }

    public void UpdatePlayerQuizStats(string uuid, int questionNum, bool answerValue, int attemptedNum)
    {
        Query playerQuery = dbQuizStatsReference.Child(uuid);

        playerQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("There was an error creating entries, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot examStats = task.Result;
                if (examStats.Exists)
                {

                    /**
                    //Update  
                    ExamStats sp = JsonUtility.FromJson<ExamStats>(examStats.GetRawJsonValue());
                    sp.attemptedNum += attemptedNum;
                    sp.updatedOn = sp.GetTimeUnix();
                    sp.attemptedOn = sp.GetTimeUnix();

                    dbQuizStatsReference.Child(uuid).Child("attemptedNum").SetValueAsync(sp.attemptedNum);

                    dbQuizStatsReference.Child(uuid).Child("qns" + questionNum + "Correct").SetValueAsync(answerValue);


                    dbQuizStatsReference.Child(uuid).Child("attemptedOn").SetValueAsync(sp.attemptedOn);
                    dbQuizStatsReference.Child(uuid).Child("updatedOn").SetValueAsync(sp.updatedOn);
                    **/
                }
            }
        });

        
    }
    /**
    public async Task<List<QuizList>> GetQuiz(int limit = 6)
    {
        List<QuizList> quizList = new List<QuizList>();
        if(limit == 4)
        {
            Query q = dbQuizReference.LimitToLast(limit);

            await q.GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("There was an error getting quiz Entries,\nError: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot ds = task.Result;

                    if (ds.Exists)
                    {
                        int rankCounter = 1;
                        foreach (DataSnapshot d in ds.Children)
                        {
                            QuizList qa = JsonUtility.FromJson<QuizList>(d.GetRawJsonValue());

                            quizList.Add(qa);
                            Debug.Log("DATA " + quizList);
                            Debug.Log(qa.questionCounter + qa.qnsDescription + qa.qnsOption1 + qa.qnsOption2);
                        }
                        foreach (QuizList qa in quizList)
                        {
                            Debug.Log(qa.questionCounter + qa.qnsDescription + qa.qnsOption1 + qa.qnsOption2);
                            rankCounter++;
                        }
                    }
                }

            });

            return quizList;
        }
        else
        {
            Query q = dbQuizReference.LimitToFirst(limit);

            await q.GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("There was an error getting quiz Entries,\nError: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot ds = task.Result;

                    if (ds.Exists)
                    {
                        int rankCounter = 1;
                        foreach (DataSnapshot d in ds.Children)
                        {
                            QuizList qa = JsonUtility.FromJson<QuizList>(d.GetRawJsonValue());

                            quizList.Add(qa);
                            Debug.Log("DATA " + quizList);
                            Debug.Log(qa.questionCounter + qa.qnsDescription + qa.qnsOption1 + qa.qnsOption2);
                        }
                        foreach (QuizList qa in quizList)
                        {
                            Debug.Log(qa.questionCounter + qa.qnsDescription + qa.qnsOption1 + qa.qnsOption2);
                            rankCounter++;
                        }
                    }
                }

            });

            return quizList;
        }
        

    }

    public void CreatePlayerQuizStats(string uuid, string userName, int attemptedNum, bool qns0Correct, bool qns1Correct, bool qns2Correct, bool qns3Correct, bool qns4Correct, bool qns5Correct,
        bool qns6Correct, bool qns7Correct, bool qns8Correct, bool qns9Correct)
    {
        //Create
        ExamStats es = new ExamStats(userName, attemptedNum, qns0Correct, qns1Correct, qns2Correct, qns3Correct, qns4Correct, qns5Correct,
            qns6Correct, qns7Correct, qns8Correct, qns9Correct);
        dbQuizStatsReference.Child(uuid).SetRawJsonValueAsync(es.SaveToJson());
    }
    **/
}