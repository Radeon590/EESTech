using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityController : MonoBehaviour
{
    public GameObject townMap;
    public GameObject matchThree;
    public GameObject account;

    void ChangeActive(GameObject gameObject)
    {
        if (Variables.ActiveActivityPanel != null) Variables.ActiveActivityPanel.SetActive(false);
        gameObject.SetActive(true);
        Variables.ActiveActivityPanel = gameObject;
    }

    public void GoToAccount()
    {
        ChangeActive(account);
    }
    public void GoToTownMap()
    {
        ChangeActive(townMap);
    }

    public void GoToMatchThree()
    {
        ChangeActive(matchThree);
        matchThree.transform.GetChild(matchThree.transform.childCount - 1).GetComponent<GameMatchThree>().Load();
    }
}
