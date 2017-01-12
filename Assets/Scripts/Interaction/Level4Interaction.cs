using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Level4Interaction : LevelInteration
{
    [SerializeField]
    private ParticleSystem huskParticles;
    [SerializeField]
    private float triggerForce = 10;

    private ParticleSystem.EmissionModule huskEmission;

    public override void Start()
    {
        base.Start();

        huskEmission = huskParticles.emission;
    }

    private void Update()
    {
        // get average hand velocities
        var average = BodyManager.GetAverageVelocity(new JointType[] { JointType.HandLeft, JointType.HandRight });

        if (average.magnitude >= triggerForce)
        {
            huskEmission.enabled = true;
            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 1, Time.deltaTime);
        }
        else
        {
            huskEmission.enabled = false;
            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 0, Time.deltaTime);
        }
    }
}
