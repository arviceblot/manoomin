using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(AudioClip))]
public class Movie : MonoBehaviour
{
    [SerializeField]
    private bool playOnStart = true;

    private MovieTexture movieTexture;
    private AudioSource movieAudio;

    // Use this for initialization
    void Start()
    {
        movieTexture = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        movieAudio = GetComponent<AudioSource>();

        if (playOnStart)
        {
            movieTexture.Play();
            movieAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        movieTexture.Stop();
        movieAudio.Stop();
    }
}
