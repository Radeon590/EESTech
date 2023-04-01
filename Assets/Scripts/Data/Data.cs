using System;
using System.Collections.Generic;
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
    public List<string> QuestionsBank;
    public int LessonsDone;
    public int HP;

    public Data(string name)
    {
        Cash = 0;
        Player = new User(name);
        QuestionsBank = new List<string>();
        LessonsDone = 0;
        HP = 50;
    }
}