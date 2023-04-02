using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMashine : MonoBehaviour
{
    [SerializeField] private Activity firstActivity;

    void Start()
    {
        InitialzeApp();
    }

    public void InitialzeApp()
    {
        if (Variables.LoadData() != 0)
        {
            Variables.CreatePlayer("пчеловек");
        }
        firstActivity.Activate();
    }
}
