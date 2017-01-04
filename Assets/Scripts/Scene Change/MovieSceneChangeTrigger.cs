using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers scene change on movie completion.
/// </summary>
public class MovieSceneChangeTrigger : SceneChangeTrigger
{
    [SerializeField]
    private Movie movie;

    private void Update()
    {
        if (!movie.IsPlaying && movie.PlayOnStart)
        {
            Trigger();
        }
    }
}
