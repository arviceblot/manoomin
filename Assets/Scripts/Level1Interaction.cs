using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Level1Interaction : MonoBehaviour
{
    [SerializeField]
    private float velocityThreshold = 5f;

    private KinectBodyManager bodyManager;

    #region MonoBehaviour

    private void Start()
    {
        if (bodyManager == null)
        {
            bodyManager = FindObjectOfType<KinectBodyManager>();
        }
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

        // check if users are moving too fast
        if (!(average.magnitude > velocityThreshold))
        {
            // do all the things here

        }
    }

    #endregion
}
