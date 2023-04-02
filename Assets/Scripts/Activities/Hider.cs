using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour
{
    public void Hide()
    {
        gameObject.SetActive(true);
    }

    public void Unhide()
    {
        gameObject.SetActive(false);
    }
}
