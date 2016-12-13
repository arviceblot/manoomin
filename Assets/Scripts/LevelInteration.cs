using UnityEngine;

public class LevelInteration : MonoBehaviour
{
    private KinectBodyManager bodyManager;
    public KinectBodyManager BodyManager
    {
        get { return this.bodyManager; }
    }

    public virtual void Start()
    {
        bodyManager = FindObjectOfType<KinectBodyManager>();
    }

    public Vector3 GetAverage(Windows.Kinect.JointType[] joints)
    {
        var average = Vector3.zero;

        foreach (var body in BodyManager.Bodies)
        {
            foreach (var joint in joints)
            {
                average += body.Velocity(joint);
            }
        }

        if (average != Vector3.zero)
        {
            average /= BodyManager.Bodies.Count * (float)joints.Length;
        }

        return average;
    }
}
