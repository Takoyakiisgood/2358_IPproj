/******************************************************************************
Team Name: 2359Studios

Author: Jordan Yeo Xiang Yu, Celest Goh Zi Xuan, Theng Sun Yu, Esther Ho Enqi, Ng Hui Ling

Name of Class: AuthManager

Description of Class: This class will handle Authentication of all users in the database.

Date Created: 16/12/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    public DatabaseReference dbReference;
    public FirebaseManager fbManager;

    public GameObject signUpPage;
    public GameObject loginPage;
    public GameObject mainMenuPage;
    public GameObject formsPage;

    public Text userName;

    public Text errorMsgContent;
    public Text errorMsgContent2;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (auth.CurrentUser != null && sceneName == "MainMenu")
        {
            formsPage.SetActive(false);
            loginPage.SetActive(false);
            mainMenuPage.SetActive(true);
            FormManager.Instance.mainMenuAnimator.SetTrigger("Up");
            userName.text = GetCurrentUserDisplayName();
            PlayerDataManager.Instance.UpdatePlayerData(GetCurrentUser());
        }
        else if (auth.CurrentUser != null)
        {
            _ = fbManager.GetPlayerData(GetCurrentUser());
        }
    }

    public async void SignUpNewUser(string userName, string email, string cultural, string food, string password, int currentDay, bool active)
    {

        FirebaseUser newPlayer = await SignUpNewUserOnly( userName, email, password);
        

            if (newPlayer != null)
            {
                await CreateNewPlayer(newPlayer.UserId, userName, newPlayer.Email, cultural, food, currentDay, active);
                await UpdatePlayerDisplayName(userName);
                mainMenuPage.SetActive(true);
                signUpPage.SetActive(false);

            }

     
        
    }
    public async Task<FirebaseUser> SignUpNewUserOnly(string userName, string email, string password)
    {

        FirebaseUser newPlayer = null;
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                if(task.Exception != null)
                {
                    string errorMsg = this.HandleSignUpError(task);
                    errorMsgContent2.text = errorMsg;
                    Debug.Log("Sign Up Error " + errorMsg);
                    errorMsgContent2.gameObject.SetActive(true);
                }

            }
            else if (task.IsCompleted)
            {
                newPlayer = task.Result;
                errorMsgContent2.gameObject.SetActive(false);
                Debug.LogFormat("New Player Details {0} {1}", newPlayer.UserId, newPlayer.Email);

                
                fbManager.UpdatePlayerStats(newPlayer.UserId, userName, 0, 0, 0, 0, 0, 0);
                //fbManager.CreatePlayerQuizStats(newPlayer.UserId, userName, 0, false, false, false, false, false, false,
            //false, false, false, false);
                PlayerDataManager.Instance.UpdatePlayerData(newPlayer.UserId);
            }
        });

        return newPlayer;
    }

    public async Task UpdatePlayerDisplayName(string displayName)
    {
        if(auth.CurrentUser != null)
        {
            UserProfile profile = new UserProfile { 
                DisplayName = displayName 
            };
            await auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was cancelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("User Profile updated Successfuly");
                Debug.LogFormat("Checking user display name from auth {0}", GetCurrentUserDisplayName());
            });

        }
    }
    public async Task SignInUser(string email, string password)
    {

            await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    string errorMsg = this.HandleSignInError(task);
                    errorMsgContent.text = errorMsg;
                    Debug.Log("Sign In ERROR: " + errorMsg);
                    errorMsgContent.gameObject.SetActive(true);
                    Debug.LogError("SignInWithEmailAndPassword Sync was cancelled");
                }
                else if (task.IsCompleted)
                {
                    FirebaseUser newPlayer = task.Result;
                    Debug.LogFormat("User signed in successfully: {0} {1}", newPlayer.DisplayName, newPlayer.UserId);
                    mainMenuPage.SetActive(true);
                    loginPage.SetActive(false);
                    errorMsgContent.gameObject.SetActive(false);
                }


                //UpdatePlayerDisplayName(newPlayer.DisplayName);
                userName.text = GetCurrentUserDisplayName();
            });

        
    }

    public void SignOutUser(string uuid, bool active)
    {
        if (auth.CurrentUser != null)
        {
            Debug.LogFormat("Auth user {0} {1}", auth.CurrentUser.UserId, auth.CurrentUser.Email);

            dbReference.Child("players/" + uuid).Child("active").SetValueAsync(active);
            auth.SignOut();
            mainMenuPage.SetActive(false);
            loginPage.SetActive(true);
            Debug.Log("Successfully Sign Out!");
            
            
        }
    }

    public void ForgetPassword(string email)
    {
        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                string errorMsg = this.HandleForgetPassword(task);
                errorMsgContent.text = errorMsg;
                Debug.Log("Forget Password ERROR: " + errorMsg);
                errorMsgContent.gameObject.SetActive(true);
            } 
            else if (task.IsCompleted)
            {
                errorMsgContent.text = "Forget password email sent successfully";
                errorMsgContent.gameObject.SetActive(true);
                Debug.Log("Forget password email sent successfully");
            }
        });
        
    }

    public async Task CreateNewPlayer(string uuid, string userName, string email, string cultural, string food, int currentDay, bool active)
    {
        PlayerData newPlayer = new PlayerData(userName, email, cultural, food, currentDay, active);
        Debug.LogFormat("Player details: {0}", newPlayer.PrintPlayerData());

        await dbReference.Child("players/" + uuid).SetRawJsonValueAsync(newPlayer.SaveToJson());

        //Update auth player with new display name
        await UpdatePlayerDisplayName(userName);
    }

    public string GetCurrentUserDisplayName()
    {
        return auth.CurrentUser.DisplayName;
    }

    public string GetCurrentUser()
    {
        string UserId = null;
        if (auth.CurrentUser!= null)
        {
            UserId = auth.CurrentUser.UserId;
        }
        

        return UserId;
    }
    public void ChangeScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }



    public string HandleSignUpError(Task task)
    {
        string errorMsg = "";

        if(task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            errorMsg = "Sign Up Fail: ";
            switch (errorCode)
            {
                case AuthError.EmailAlreadyInUse:
                    errorMsg += "Email is already in use.";
                    break;
                case AuthError.WeakPassword:
                    errorMsg += "Weak Password. Use at least 8 characters";
                    break;
                case AuthError.MissingPassword:
                    errorMsg += "Password is missing";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Email is Invalid";
                    break;
                default:
                    errorMsg += errorCode;
                    break;
            }
            Debug.Log("Error message" + errorMsg);
        }
        return errorMsg;
    }

    public string HandleSignInError(Task<FirebaseUser> task)
    {
        string errorMsg = "";

        if (task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            errorMsg = "Sign In Fail: ";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    errorMsg += "Email not found";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Email is Invalid";
                    break;
                case AuthError.WrongPassword:
                    errorMsg += "Password is Incorrect";
                    break;
                case AuthError.UserNotFound:
                    errorMsg += "User is invalid";
                    break;
                default:
                    errorMsg += errorCode;
                    break;
            }
            Debug.Log("Error message" + errorMsg);
        }
        return errorMsg;
    }

    public string HandleForgetPassword(Task task)
    {
        string errorMsg = "";

        if (task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    errorMsg += "Email not found";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Email is Invalid";
                    break;
                case AuthError.UserNotFound:
                    errorMsg += "User is invalid";
                    break;
                default:
                    errorMsg += errorCode;
                    break;
            }
            Debug.Log("Error message" + errorMsg);
        }
        return errorMsg;
    }
}
