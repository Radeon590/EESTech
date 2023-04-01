using System;
using UnityEngine;

[Serializable]
public class User
{
    public string Name;

    public User(string name)
    {
        Name = name;
    }
}



[Serializable]
public class Data
{
    public int Cash;
    public User Player;

    public Data(string name)
    {
        Cash = 0;
        Player = new User(name);
    }
}