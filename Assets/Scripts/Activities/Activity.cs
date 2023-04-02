using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activity : MonoBehaviour
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public bool NeedDeactivateGameObject = true;

    public void Activate()
    {
        ActivitiesController.CurrentActivity?.Deactivate();
        OnActivate?.Invoke();
        ActivitiesController.CurrentActivity = this;
        if (NeedDeactivateGameObject)
        {
            gameObject.SetActive(true);
        }
        
    }

    public void Deactivate()
    {
        if(ActivitiesController.CurrentActivity == this)
            ActivitiesController.CurrentActivity = null;
        OnDeactivate?.Invoke();
        if (NeedDeactivateGameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
