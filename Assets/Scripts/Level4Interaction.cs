using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Level4Interaction : MonoBehaviour
{
    [SerializeField]
    private Vector2 maxPosition = new Vector2(1, 1);
    [SerializeField]
    [SerializeField]
    private GameObject basket;

    private KinectBodyManager bodyManager;

    // Use this for initialization
    private void Start()
    {
        bodyManager = FindObjectOfType<KinectBodyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // get average hand velocities
        var average = Vector3.zero;
        foreach (var body in bodyManager.Bodies)
        {
            average += body.Velocity(JointType.HandLeft);
            average += body.Velocity(JointType.HandRight);
        }
        if (average != Vector3.zero)
        {
            average /= bodyManager.Bodies.Count * 2f;
        }

        // clamp
        basket.transform.position = new Vector3(
            Mathf.Clamp(basket.transform.position.x + average.x, minPosition.x, maxPosition.x),
            Mathf.Clamp(basket.transform.position.y + average.y, minPosition.y, maxPosition.y));
    }
}
