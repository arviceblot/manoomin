using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Collections.Generic;

public class Level1Interaction : LevelInteration
{
    [SerializeField]
    private float velocityThreshold = 5f;
    [SerializeField]
    private float minimumVelocity = 2f;
    [SerializeField]
    private float spawnInterval = 1f;
    [SerializeField]
    private List<ParticleSystem> harvestParticles;

    private List<ParticleSystem.EmissionModule> harvestEmissions;

    private float lastSpawnTime;

    #region MonoBehaviour

    public override void Start()
    {
        base.Start();
        lastSpawnTime = spawnInterval;

        harvestEmissions = new List<ParticleSystem.EmissionModule>();
        harvestParticles.ForEach(p => harvestEmissions.Add(p.emission));
    }

    private void Update()
    {
        // get the average velocity of each body's hands
        var average = BodyManager.GetAverageVelocity(new JointType[] { JointType.HandLeft, JointType.HandRight });

        lastSpawnTime += Time.deltaTime;

        // check if users are moving too fast
        if (average.magnitude < velocityThreshold && average.magnitude > minimumVelocity)
        {
            // do all the things here
            if (lastSpawnTime > spawnInterval)
            {
                harvestEmissions.ForEach(e => e.enabled = true);
                lastSpawnTime = 0f;
            }
        }
        else
        {
            // don't spawn rice
            harvestEmissions.ForEach(e => e.enabled = false);
        }
    }

    #endregion
}
