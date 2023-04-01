using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Variables
{
    private const string SAVE_PATH = "/Data.dat";

    private static Data data;

    public static Data Data
    {
        get { return data; }
        set { data = value; }
    }

    public static User GetPlayer()
    {
        if (data == null)
        {
            if (LoadData() == null)
            {
                data = new("User name");
            }
        }
        return data.Player;
    }

    public static void AddCash(float added)
    {
        if (data!= null) {
            data.Cash += Mathf.FloorToInt(added);
            SaveData();
        }
        
    }

    public static int GetCash()
    {
        return data.Cash;
    }

    public static void CreatePlayer(string name)
    {
        data = new(name);
        SaveData();
    }

    private static void SaveData()
    {
        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + SAVE_PATH);
        bf.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Load data from device memory.
    /// </summary>
    /// <returns>Return 0 if secces, null if no data</returns>
    /// <exception cref="Exception"></exception>
    public static int? LoadData()
    {
        int? res = null;
        try
        {
            if (!File.Exists(Application.persistentDataPath + SAVE_PATH)) throw new Exception();
            BinaryFormatter bf = new();
            FileStream file = File.Open(Application.persistentDataPath + SAVE_PATH, FileMode.Open);
            data = (Data)bf.Deserialize(file);
            file.Close();
            res = 0;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
        return res;
    }
}
