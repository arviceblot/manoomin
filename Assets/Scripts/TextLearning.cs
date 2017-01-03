using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class manages the playback of text with audio at specific times.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class TextLearning : MonoBehaviour
{
    [SerializeField]
    private Text anishinaabemowinText;
    [SerializeField]
    private AudioClip anishinaabemowinAudio;
    [SerializeField]
    private Text englishText;
    [SerializeField]
    private AudioClip englishAudio;

    [SerializeField]
    [Tooltip("The time to wait before we start the fade in animation.")]
    private float startTime;
    [SerializeField]
    [Tooltip("Duration in seconds to display the text once fully visible.")]
    private float holdDuration;
    [SerializeField]
    [Tooltip("Duration in seconds to fade the text in and out.")]
    private float fadeDuration;

    private AudioSource audioSource;

    void Start()
    {
        anishinaabemowinText.GetComponent<CanvasRenderer>().SetAlpha(0);
        englishText.GetComponent<CanvasRenderer>().SetAlpha(0);
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(Fade());
    }

    /// <summary>
    /// Coroutine for handling main playback functionality.
    /// </summary>
    /// <returns>
    /// Nothing.
    /// </returns>
    private IEnumerator Fade()
    {
        // wait for start time
        yield return new WaitForSeconds(startTime);

        // fade in
        anishinaabemowinText.CrossFadeAlpha(1f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        // hold for duration of audio clip
        audioSource.PlayOneShot(anishinaabemowinAudio);
        yield return new WaitForSeconds(anishinaabemowinAudio.length);

        // fade out
        anishinaabemowinText.CrossFadeAlpha(0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        // fade in english
        englishText.CrossFadeAlpha(1f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        // hold for duration
        audioSource.PlayOneShot(englishAudio);
        yield return new WaitForSeconds(englishAudio.length);

        // fade out
        englishText.CrossFadeAlpha(0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);
    }
}
