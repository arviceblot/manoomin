using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Level4Interaction : LevelInteration
{
    [SerializeField]
    private Vector2 maxPosition = new Vector2(1, 1);
    [SerializeField]
    private Vector2 minPosition = new Vector2(-1, -1);
    [SerializeField]
    private GameObject basket;

    // Update is called once per frame
    void Update()
    {
        // get average hand velocities
        var average = GetAverage(new JointType[] { JointType.HandLeft, JointType.HandRight });

        // clamp
        basket.transform.position = new Vector3(
            Mathf.Clamp(basket.transform.position.x + average.x, minPosition.x, maxPosition.x),
            Mathf.Clamp(basket.transform.position.y + average.y, minPosition.y, maxPosition.y));
    }
}
