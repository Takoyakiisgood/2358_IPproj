using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public int pfp;
    public bool active;
    public long createdOn;
    public long lastLoggedIn;
    public long updatedOn;
    public string userName;
    public string cultural;
    public string email;
    public string food;

    public User()
    {

    }
    public User(string userName, string email, string cultural, 
        string food, bool active, int pfp, long lastLoggedIn, long createdOn, long updatedOn)
    {
        this.userName = userName;
        this.email = email;
        this.cultural = cultural;
        this.food = food;
        this.active = active;
        this.pfp = pfp;

        var timestamp = this.GetTimeUnix();
        this.lastLoggedIn = timestamp;
        this.createdOn = timestamp;
        this.updatedOn = timestamp;
    }
    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }
}