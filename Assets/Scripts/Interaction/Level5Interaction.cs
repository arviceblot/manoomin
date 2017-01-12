using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Windows.Kinect;

public class Level5Interaction : LevelInteration
{
    [SerializeField]
    private ParticleSystem aurora;
    [SerializeField]
    private Movie movie;
    [SerializeField]
    private Text debugText;

    private ParticleSystem.EmissionModule auroraEmission;
    private float movieLength;

    public override void Start()
    {
        base.Start();

        if (ApplicationManager.Instance.UseDebugeMode)
        {
            debugText.enabled = true;
        }

        auroraEmission = aurora.emission;

        // ensure emmision is disabled at start
        auroraEmission.enabled = false;

        movieLength = movie.MovieTexture.duration;
    }

    private void Update()
    {
        // the number of users with hands above heads
        var countAbove = 0f;

        foreach (var body in BodyManager.Bodies)
        {
            if (body.Position(JointType.HandLeft).y > body.Position(JointType.Head).y
                && body.Position(JointType.HandRight).y > body.Position(JointType.Head).y)
            {
                countAbove++;
            }
        }

        debugText.text = "Users above: " + countAbove;

        // make sure we fade out the song at the end
        if (PlayTime + 10 > movieLength)
        {
            auroraEmission.enabled = false;
            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 0, Time.deltaTime);
        }
        else if (countAbove / (float)BodyManager.Bodies.Count >= (float)BodyManager.Bodies.Count / 2f)
        {
            // more than half
            //gift.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, scale * Time.deltaTime));
            //debugText.text += " Force: " + scale * Time.deltaTime;
            auroraEmission.enabled = true;
            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 1, Time.deltaTime);
        }
        else
        {
            auroraEmission.enabled = false;
            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 0, Time.deltaTime);
        }

    }
}
