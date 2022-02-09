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
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading.Tasks;

public class FormManager : MonoBehaviour
{
    public static FormManager Instance;
    public InputField emailInput;
    public InputField passwordInput;

    public InputField emailInput1;
    public InputField passwordInput1;
    public InputField userNameInput;

    public AuthManager authManager;

    public Animator mainMenuAnimator;

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

        authManager.SignUpNewUser(userName, email, cultural, food, password, currentDay, active);
        Debug.Log("Sign Up...");
        mainMenuAnimator.SetTrigger("Up");
    }

    public void OnLogIn()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();
        _ = authManager.SignInUser(email, password);
        Debug.Log("Log In...");
        mainMenuAnimator.SetTrigger("Up");
    }
    public void OnSignOut()
    {
        bool active = false;
        
        authManager.SignOutUser(authManager.GetCurrentUser(), active);
        Debug.Log("Signing Out...");
        emailInput.text = "";
        passwordInput.text = "";
        mainMenuAnimator.SetTrigger("Down");
    }

    public void OnForgetPassword()
    {
        string email = emailInput.text.Trim();
        authManager.ForgetPassword(email);
        Debug.Log("Sending Forget Password Email...");
    }

}
