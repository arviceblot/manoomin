using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers a scene change after a specific duration.
/// </summary>
public class TimeSceneChangeTrigger : SceneChangeTrigger
{
    [SerializeField]
    private bool startOnStart = true;
    [SerializeField]
    private float startTime = 0f;
    [SerializeField]
    private float duration = 180f;

    private IEnumerator Start()
    {
        if (startOnStart)
        {
            // start timer now
            yield return new WaitForSeconds(duration);
        }
        else
        {
            // wait to start timer until start time
            yield return new WaitForSeconds(startTime + duration);
        }

        // now we can trigger the scene change
        Trigger();
    }
}
