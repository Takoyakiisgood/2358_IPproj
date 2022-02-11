using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    private string AuthKey = "AIzaSyAypqI1GuYE9cqqU_Zd6SUsi7daiiomt1s";
    private string idToken; //auth ID for newly created user
    private string localId; //UserID
    private static string playerName;
    private string databaseURL = "https://mesakanharmoni-default-rtdb.asia-southeast1.firebasedatabase.app/players/";
    
    // Start is called before the first frame update
    void Start()
    {
        SignUpUserButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignUpUserButton()
    {
        SignUpUser("test7@gmail.com", "test7", "abc124");
    }

    private void PostToDatabase(bool emptyScore = false)
    {
        User user = new User();

        if (emptyScore)
        {
            user.userScore = 0;
            user.userName = "Huiling";
        }

        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, user);
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
                PostToDatabase(true);

            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }
}
