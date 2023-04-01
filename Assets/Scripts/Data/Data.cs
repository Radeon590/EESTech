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
public class BuildInfo
{
    public bool BuildMatchThree;
    public bool Build1;
    public bool Build2;
    public bool Build3;
    public bool Build4;

    public BuildInfo()
    {
        BuildMatchThree = false;
        Build1 = false;
        Build2 = false;
        Build3 = false;
        Build4 = false;
    }
}

[Serializable]
public class Data
{
    public int Cash;
    public User User;
    public BuildInfo BuildInfo;

    public Data(string name)
    {
        Cash = 0;
        User = new User(name);
        BuildInfo= new BuildInfo();
    }

}