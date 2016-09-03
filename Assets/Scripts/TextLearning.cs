using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextLearning : MonoBehaviour
{
    [SerializeField]
    private Text anishinaabemowinText;
    [SerializeField]
    private Text englishText;

    [SerializeField]
    [Tooltip("The time to wait before we start the fade in animation.")]
    private float startTime;
    [SerializeField]
    [Tooltip("Duration in seconds to display the text once fully visible.")]
    private float holdDuration;
    [SerializeField]
    [Tooltip("Duration in seconds to fade the text in and out.")]
    private float fadeDuration;

    // Use this for initialization
    void Start()
    {
        anishinaabemowinText.GetComponent<CanvasRenderer>().SetAlpha(0);
        englishText.GetComponent<CanvasRenderer>().SetAlpha(0);

        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        // wait for start time
        yield return new WaitForSeconds(startTime);

        // fade in
        anishinaabemowinText.CrossFadeAlpha(1f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        // hold for duration
        yield return new WaitForSeconds(holdDuration);

        // fade out
        anishinaabemowinText.CrossFadeAlpha(0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        // fade in english
        englishText.CrossFadeAlpha(1f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        // hold for duration
        yield return new WaitForSeconds(holdDuration);

        // fade out
        englishText.CrossFadeAlpha(0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);
    }
}
