using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Level1Interaction : MonoBehaviour
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

    private KinectBodyManager bodyManager;
    private float lastSpawnTime;

    #region MonoBehaviour

    private void Start()
    {
        bodyManager = FindObjectOfType<KinectBodyManager>();
        lastSpawnTime = spawnInterval;
    }

    private void Update()
    {
        var average = Vector3.zero;

        // get the average velocity of each body's hands
        foreach (var body in bodyManager.Bodies)
        {
            average += body.Velocity(JointType.HandLeft);
            average += body.Velocity(JointType.HandRight);
        }

        average /= bodyManager.Bodies.Count * 2f;

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
