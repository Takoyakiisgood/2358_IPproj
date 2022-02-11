using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public Text scoreText;
    public InputField getScoreText;

    public InputField emailText;
    public InputField usernameText;
    public InputField passwordText;

    private System.Random random = new System.Random();

    User user = new User();

    private string databaseURL = "https://mesakanharmoni-default-rtdb.asia-southeast1.firebasedatabase.app/players/";
    private string AuthKey = "AIzaSyAypqI1GuYE9cqqU_Zd6SUsi7daiiomt1s";



    public static int playerScore;
    public static string playerName;

    private string idToken;

    public static string localId;

    private string getLocalId;


    private void Start()
    {
        playerScore = random.Next(0, 101);
        scoreText.text = "Score: " + playerScore;
    }

    private void Update()
    {
        SignUpUserButton();
    }
    public void OnSubmit()
    {
        PostToDatabase();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + user.userScore;
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

    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response =>
        {
            user = response;
            UpdateScore();
        });
    }

    public void SignUpUserButton()
    {
        SignUpUser("test3@gmail.com", "test3", "abc123");
    }

    public void SignInUserButton()
    {
        SignInUser(emailText.text, passwordText.text);
    }

    private void SignUpUser(string email, string username, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                playerName = username;
                PostToDatabase(true);

            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    private void SignInUser(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                GetUsername();
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    private void GetUsername()
    {
        RestClient.Get<User>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            playerName = response.userName;
        });
    }

}