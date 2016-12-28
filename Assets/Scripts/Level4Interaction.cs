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

    void Update()
    {
        // get average hand velocities
        var average = GetAverage(new JointType[] { JointType.HandLeft, JointType.HandRight });

        if (average.magnitude >= triggerForce)
        {
            huskEmission.enabled = true;
        }
        else
        {
            huskEmission.enabled = false;
        }
    }
}
