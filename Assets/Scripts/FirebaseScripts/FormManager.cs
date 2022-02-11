/******************************************************************************
Team Name: 2359Studios

Author: Jordan Yeo Xiang Yu, Celest Goh Zi Xuan, Theng Sun Yu, Esther Ho Enqi, Ng Hui Ling

Name of Class: FormManager

Description of Class: This class will handle the sign in, sign out, forget password, sign up using firebase database

Date Created: 16/12/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using System.Threading.Tasks;
using Proyecto26;

public class FormManager : MonoBehaviour
{
    public static FormManager Instance;
    public InputField emailInput;
    public InputField passwordInput;

    public InputField emailInput1;
    public InputField passwordInput1;
    public InputField userNameInput;

    public static int playerScore;
    public static string playerName;

    private string idToken;

    public static string localId;

    private string getLocalId;

    public Animator mainMenuAnimator;

    private string databaseURL = "https://mesakanharmoni-default-rtdb.asia-southeast1.firebasedatabase.app/players";
    private string AuthKey = "AIzaSyAypqI1GuYE9cqqU_Zd6SUsi7daiiomt1s";
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValidateEmail()
    {
        Debug.Log("Perfomring email validation...");
    }

    public void OnSignUp()
    {
        string email = emailInput1.text.Trim();
        string password = passwordInput1.text.Trim();
        string userName = userNameInput.text;
        string cultural = "";
        string food = "";

        int currentDay = 1;
        bool active = true;

        Debug.Log("Sign Up...");
        mainMenuAnimator.SetTrigger("Up");
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                playerName = userName;
                PostToDatabase(true);

            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    private void PostToDatabase(bool emptyScore = false)
    {
        User user = new User();

        if (emptyScore)
        {
            user.userScore = 0;
        }

        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, user);
    }

    public void OnLogIn()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();
        Debug.Log("Log In...");
        mainMenuAnimator.SetTrigger("Up");
    }
    public void OnSignOut()
    {
        bool active = false;
        
        Debug.Log("Signing Out...");
        emailInput.text = "";
        passwordInput.text = "";
        mainMenuAnimator.SetTrigger("Down");
    }

    public void OnForgetPassword()
    {
        string email = emailInput.text.Trim();
        Debug.Log("Sending Forget Password Email...");
    }

}
