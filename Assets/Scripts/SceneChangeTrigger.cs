using UnityEngine;
using System;

public class SceneChangeTrigger : MonoBehaviour
{
    public event Action OnTrigger;

    protected virtual void Trigger()
    {
        if (OnTrigger != null)
        {
            OnTrigger();
        }
    }
}
