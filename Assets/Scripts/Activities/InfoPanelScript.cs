using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelScript : MonoBehaviour
{
    public Text coin;
    public Text HP;
    // Update is called once per frame
    void Update()
    {
        coin.text = Variables.Data.Cash.ToString();
        HP.text = Variables.Data.HP.ToString();
    }
}
