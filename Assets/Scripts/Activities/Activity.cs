using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activity : MonoBehaviour
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public void Activate()
    {
        ActivitiesController.CurrentActivity?.Deactivate();
        OnActivate.Invoke();
        ActivitiesController.CurrentActivity = this;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        if(ActivitiesController.CurrentActivity == this)
            ActivitiesController.CurrentActivity = null;
        OnDeactivate.Invoke();
        gameObject.SetActive(false);
    }
}
