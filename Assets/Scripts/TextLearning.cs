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
    [Tooltip("The time to start the fade in animation.")]
    private float startTime;
    [SerializeField]
    [Tooltip("Duration in seconds to display the text once fully visible.")]
    private float holdDuration;
    [SerializeField]
    [Tooltip("Duration in seconds to fade the text in and out.")]
    private float fadeDuration;

    // the time the script started, used for determining when to start the fade in
    private float scriptStartTime;

    // Use this for initialization
    void Start()
    {
        anishinaabemowinText.color = Color.clear;
        englishText.color = Color.clear;

        StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Fade()
    {
        scriptStartTime = Time.time;
        Debug.Log("Waiting for start time.");
        yield return new WaitForSeconds(scriptStartTime + startTime);

        Debug.Log("Starting fade in.");

        var fadeInStartTime = Time.time;
        var fadeInSpeed = 256f / fadeDuration;
        Debug.Log("Fade speed: " + fadeInSpeed);
        //yield return new WaitForSeconds(0.01f);
        while (fadeDuration > Time.time - fadeInStartTime)
        {
            Color.Lerp(anishinaabemowinText.color, Color.white, fadeInSpeed * Time.deltaTime);
            Debug.Log("Fading in.");
            yield return null;
        }

        Debug.Log("Holding.");
        yield return new WaitForSeconds(holdDuration);
    }
}
