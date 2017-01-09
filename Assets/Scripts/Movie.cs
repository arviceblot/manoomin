using UnityEngine;

/// <summary>
/// Handles movie playback.
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(AudioClip))]
public class Movie : MonoBehaviour
{
    [SerializeField]
    private bool playOnStart = true;

    private bool isPlaying;

    /// <summary>
    /// Gets if the movie is set to play on start.
    /// </summary>
    public bool PlayOnStart
    {
        get { return playOnStart; }
    }

    /// <summary>
    /// Gets if the movie is playing.
    /// </summary>
    public bool IsPlaying
    {
        get { return isPlaying && movieTexture.isPlaying; }
    }

    private MovieTexture movieTexture;
    private AudioSource movieAudio;

    private void Start()
    {
        movieTexture = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        movieAudio = GetComponent<AudioSource>();

        if (movieAudio.clip == null)
        {
            movieAudio.clip = movieTexture.audioClip;
        }

        isPlaying = false;

        if (playOnStart)
        {
            movieTexture.Play();
            isPlaying = true;
            movieAudio.Play();
        }
    }

    private void OnDestroy()
    {
        movieTexture.Stop();
        movieAudio.Stop();
    }
}
