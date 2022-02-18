using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    //Login variables
    [Header("Login")]
    public TMP_InputField L_tbEmail;
    public TMP_InputField L_tbPswd;
    public TMP_Text L_errorMsg;

    //SignUp variables
    [Header("SignUp")]
    public TMP_InputField S_tbEmail;
    public TMP_InputField S_tbUsername;
    public TMP_InputField S_tbPswd;
    public TMP_Text S_errorMsg;

    private System.Random random = new System.Random();

    User user = new User();
    playerStats playerStats = new playerStats();
    chineseCultureStats chineseCultureStats = new chineseCultureStats();
    private string databaseURL = "https://mesakanharmoni-default-rtdb.asia-southeast1.firebasedatabase.app/players/";
    private string playerStatsURL = "https://mesakanharmoni-default-rtdb.asia-southeast1.firebasedatabase.app/playerStats/";
    private string chineseCultureStatsURL = "https://mesakanharmoni-default-rtdb.asia-southeast1.firebasedatabase.app/chineseCultureStats/";
    private string AuthKey = "AIzaSyAypqI1GuYE9cqqU_Zd6SUsi7daiiomt1s";

    public static int playerScore;
    public static string playerName;
    [SerializeField]
    private string idToken;

    public string localId;

    private string getLocalId;

    public GameObject menuUI;
    public GameObject loginUI;
    public GameObject signUpUI;

    public bool attempted;
    private void Start()
    {
        instance = this;
        L_errorMsg.text = "";
        S_errorMsg.text = "";

    }

    private void Update()
    {
        
    }

    private void PostToDatabase(string email, string username)
    {
        
        user.userName = username;
        user.email = email;
        user.cultural = null;
        user.food = null;
        user.active = true;
        user.pfp = 1;
        user.lastLoggedIn = user.GetTimeUnix();
        user.createdOn = user.GetTimeUnix();
        user.updatedOn = user.GetTimeUnix();
        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, user);
    }

    private void PostToPlayerStatsDatabase()
    {
        playerStats.chineseCultureTimeTaken = 0;
        playerStats.eurasianCultureTimeTaken = 0;
        playerStats.indianCultureTimeTaken = 0;
        playerStats.malayCultureTimeTaken = 0;
        playerStats.username = user.userName;
        playerStats.dishesCompletedChinese = 0;
        playerStats.dishesCompletedEurasian = 0;
        playerStats.dishesCompletedIndian = 0;
        playerStats.dishesCompletedMalay = 0;
        playerStats.totalMistakesMade = 0;
        playerStats.createdOn = playerStats.GetTimeUnix();
        playerStats.updatedOn = playerStats.GetTimeUnix();
        RestClient.Put(playerStatsURL + "/" + localId + ".json?auth=" + idToken, playerStats);
    }
    private void PostToChineseCultureStatsDatabase()
    {
        chineseCultureStats.totalTimeTaken = 0;
        chineseCultureStats.completed = false;
        chineseCultureStats.mistakesMade = 0;
        chineseCultureStats.createdOn = chineseCultureStats.GetTimeUnix();
        chineseCultureStats.updatedOn = chineseCultureStats.GetTimeUnix();
        chineseCultureStats.username = user.userName;
        RestClient.Put(chineseCultureStatsURL + "/" + "tangYuan" + "/"+ localId + ".json?auth=" + idToken, chineseCultureStats);
    }
    public void GetPlayerStats(string localId, string idToken)
    {
        RestClient.Get<playerStats>(playerStatsURL  + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            playerStats = response;

        }).Catch(error =>
        {
            Debug.Log(error);
        });
    }
    public void SetChineseCulturalStats(float timeTaken, int mistakes, bool isComplete)
    {

        chineseCultureStats.updatedOn = chineseCultureStats.GetTimeUnix();
        chineseCultureStats.username = user.userName;
        var food = user.food;
        if (user.food == "Tang Yuan")
        {
            food = "tangYuan";
        }
        chineseCultureStats.mistakesMade += mistakes;
        chineseCultureStats.totalTimeTaken += timeTaken;
        chineseCultureStats.completed = isComplete;

        RestClient.Put(chineseCultureStatsURL + "/" + food + "/" + localId + ".json?auth=" + idToken, chineseCultureStats);
    }
    private void GetChineseCulturalStats(string localId, string idToken)
    {
        var food = user.food;
        if(user.food =="Tang Yuan")
        {
            food = "tangYuan";
        }
        RestClient.Get<chineseCultureStats>(chineseCultureStatsURL + "/" + food + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            chineseCultureStats = response;

        });
    }
    public void SetPlayerStats(float timeTaken, int mistakes)
    {
        playerStats.username = user.userName;
        playerStats.updatedOn = playerStats.GetTimeUnix();
        if(user.cultural == "Chinese")
        {
            playerStats.chineseCultureTimeTaken += timeTaken;
            
            if (chineseCultureStats.completed == true)
            {
                if (attempted == false)
                {
                    playerStats.dishesCompletedChinese += 1;
                    attempted = true;
                }
            }

        }

        playerStats.totalMistakesMade += mistakes;

        RestClient.Put(playerStatsURL + "/" + localId + ".json?auth=" + idToken, playerStats);
    }

    private void GetPlayers(string localId, string idToken)
    {
        RestClient.Get<User>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            user = response;
            Debug.Log(user.email);
            Debug.Log(user.lastLoggedIn);
        });
    }
    private void UpdatePlayers(string localId, string idToken)
    {
        user.active = true;
        user.lastLoggedIn = user.GetTimeUnix();
        user.updatedOn = user.GetTimeUnix();
        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, user);
    }
    public void SetCulture(string culture)
    {
        user.cultural = culture;
        user.updatedOn = user.GetTimeUnix();
        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, user);
    }
    public void SetFood(string food)
    {
        user.food = food;
        user.updatedOn = user.GetTimeUnix();
        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, user);
    }
    public void SignUpUserButton()
    {
        //clear error msg
        S_errorMsg.text = "";

        //get the text values
        string email = S_tbEmail.text.Trim();
        string password = S_tbPswd.text.Trim();
        string username = S_tbUsername.text;
        //check validation
        if (Validated(email, password, username))
        {
            SignUpUser(email, username, password);

        }
       
    }

    public void SignInUserButton()
    {
        //clear error msg
        L_errorMsg.text = "";

        //trim gets rid of empty space
        var email = L_tbEmail.text.Trim();
        var password = L_tbPswd.text.Trim();
        if(L_Validated(email, password))
        {
            SignInUser(email, password);
        }
        
    }

    private void SignUpUser(string email, string username, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        Debug.Log(userData);
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                playerName = username;
                menuUI.SetActive(true);
                signUpUI.SetActive(false);
                PostToDatabase(email, username);
                PostToPlayerStatsDatabase();
                PostToChineseCultureStatsDatabase();

            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }
    private IEnumerator WaitForSecs(float duration)
    {
        UpdatePlayers(localId, idToken);
        ;
        yield return new WaitForSeconds(duration);
    }
    private void SignInUser(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId; //userid
                GetPlayers(localId, idToken);
                WaitForSecs(2.0f);
                GetPlayerStats(localId, idToken);
                GetChineseCulturalStats(localId, idToken);
                menuUI.SetActive(true);
                loginUI.SetActive(false);
            }).Catch(error =>
            {
                Debug.Log("Invalid");
                L_errorMsg.text = "Wrong Email or Password.";
                L_tbEmail.text = "";
                L_tbPswd.text = "";
            });
    }
    public bool Validated(string email, string password, string username)
    {
        bool isValid = false;
        string errorMsg = "";
        //for all email have @
        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        if (email == "" || password == "" || username == "")
        {
            errorMsg = "Please fill in the blanks";
            //display error message
            S_errorMsg.text = "Error: " + errorMsg;
        }
        else
        {
            if (!Regex.IsMatch(email, pattern, options))
            {
                errorMsg += " Invalid email";
                S_tbEmail.text = "";
            }
            else
            {
                if (password.Length < 6)
                {
                    errorMsg += " Password should be at least 6 characters";
                    S_tbPswd.text = "";
                }
                else
                {
                    isValid = true;
                }
            }
        }

        if (isValid)
        {
            //set the error msg to nothing
            S_errorMsg.text = "";
        }
        else
        {
            //display error message
            S_errorMsg.text = "Error: " + errorMsg;
        }
        Debug.Log(isValid);
        return isValid;
    }

    public bool L_Validated(string email, string password)
    {
        bool isValid = false;
        string errorMsg = "";
        //for all email have @
        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        if (email == "" || password == "")
        {
            errorMsg = "Please fill in the blanks";
            //display error message
            L_errorMsg.text = "Error: " + errorMsg;
        }
        else
        {
            if (!Regex.IsMatch(email, pattern, options))
            {
                errorMsg += " Invalid email";
                L_tbEmail.text = "";
            }
            else
            {
                if (password.Length < 6)
                {
                    errorMsg += " Password should be at least 6 characters";
                    L_tbPswd.text = "";
                }
                else
                {
                    isValid = true;
                }
            }
        }

        if (isValid)
        {
            //set the error msg to nothing
            L_errorMsg.text = "";
        }
        else
        {
            //display error message
            L_errorMsg.text = "Error: " + errorMsg;
        }
        Debug.Log(isValid);
        return isValid;
    }
    

}