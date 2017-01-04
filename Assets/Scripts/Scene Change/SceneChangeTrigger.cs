using UnityEngine;
using System;

/// <summary>
/// Base class for triggering scene changes.
/// </summary>
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
