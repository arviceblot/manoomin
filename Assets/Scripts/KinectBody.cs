using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Collections.Generic;
using System.Linq;

public class KinectBody : MonoBehaviour
{
    private static int POSITIONS = 5;

    private ulong id;
    private Body body;

    /// <summary>
    /// Current positions of the joints in corrected space.
    /// </summary>
    private IDictionary<Windows.Kinect.JointType, Vector3> positions;
    /// <summary>
    /// List of last N positions used for calculating velocities.
    /// </summary>
    private IDictionary<Windows.Kinect.JointType, IList<Vector3>> lastNpositions;
    /// <summary>
    /// Delta times for the last N positions, also used for velocity.
    /// </summary>
    private IList<float> times;

    public ulong Id
    {
        get { return id; }
    }

    public Body Body
    {
        get { return body; }
        set
        {
            body = value;
            id = body.TrackingId;
        }
    }

    #region MonoBehaviour

    private void Start()
    {
        positions = new Dictionary<JointType, Vector3>();

        lastNpositions = new Dictionary<JointType, IList<Vector3>>();
        for (JointType joint = JointType.SpineBase; joint <= JointType.ThumbRight; joint++)
        {
            lastNpositions[joint] = new List<Vector3>(POSITIONS);
        }

        times = new List<float>(POSITIONS);
    }

    public void Update()
    {
        // update positions of all parts
        for (JointType joint = JointType.SpineBase; joint <= JointType.ThumbRight; joint++)
        {
            positions[joint] = ProjectJointPosition(body.Joints[joint]);

            // remove the last item
            if (lastNpositions[joint].Count == POSITIONS)
            {
                lastNpositions[joint].RemoveAt(POSITIONS - 1);
            }
            // add new one to the front
            lastNpositions[joint].Insert(0, positions[joint]);

            // update times
            if (times.Count == POSITIONS)
            {
                times.RemoveAt(POSITIONS - 1);
            }
            times.Insert(0, Time.deltaTime);
        }
    }

    #endregion

    public Vector3 Velocity(JointType joint)
    {
        var average = Vector3.zero;
        foreach (var point in lastNpositions[joint])
        {
            average += point;
        }
        var averagePosition = average / lastNpositions[joint].Count;
        return averagePosition / times.Sum();
    }

    private static Vector3 ProjectJointPosition(Windows.Kinect.Joint joint, float z = 0f)
    {
        // TODO: do projection space calculations here instead of 10x values
        return new Vector3(joint.Position.X, joint.Position.Y * 10, z);
    }
}
