using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Level1Interaction : LevelInteration
{
    [SerializeField]
    private float velocityThreshold = 5f;
    [SerializeField]
    private float minimumVelocity = 2f;
    [SerializeField]
    private float spawnInterval = 1f;
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private GameObject spawnPrefab;

    private float lastSpawnTime;

    #region MonoBehaviour

    public override void Start()
    {
        base.Start();
        lastSpawnTime = spawnInterval;
    }

    private void Update()
    {
        // get the average velocity of each body's hands
        var average = GetAverage(new JointType[] { JointType.HandLeft, JointType.HandRight });

        lastSpawnTime += Time.deltaTime;

        // check if users are moving too fast
        if (average.magnitude < velocityThreshold && average.magnitude > minimumVelocity)
        {
            // do all the things here
            if (lastSpawnTime > spawnInterval)
            {
                Instantiate(spawnPrefab, spawnPosition);
                lastSpawnTime = 0f;
            }
        }
    }

    #endregion
}
