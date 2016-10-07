using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Collections.Generic;
using System.Linq;
using System;

public class KinectBody : MonoBehaviour
{
    private static int POSITIONS = 5;

    private ulong id;
    private Body body;

    /// <summary>
    /// Current positions of the joints in corrected space.
    /// </summary>
    private Dictionary<JointType, Vector3> positions;
    /// <summary>
    /// List of last N positions used for calculating velocities.
    /// </summary>
    private Dictionary<JointType, LinkedList<Vector3>> lastNpositions;
    /// <summary>
    /// Delta times for the last N positions, also used for velocity.
    /// </summary>
    private LinkedList<float> times;

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

        lastNpositions = new Dictionary<JointType, LinkedList<Vector3>>();
        foreach (JointType joint in Enum.GetValues(typeof(JointType)))
        {
            lastNpositions[joint] = new LinkedList<Vector3>();
            for (int i = 0; i < POSITIONS; i++)
            {
                lastNpositions[joint].AddFirst(new Vector3());
            }
        }

        times = new LinkedList<float>();
        for (int i = 0; i < POSITIONS; i++)
        {
            times.AddFirst(0f);
        }
    }

    public void Update()
    {
        // update positions of all parts
        foreach (JointType joint in Enum.GetValues(typeof(JointType)))
        {
            positions[joint] = ProjectJointPosition(body.Joints[joint]);

            // add new one to the front
            lastNpositions[joint].AddFirst(positions[joint]);
            // remove the last item
            lastNpositions[joint].RemoveLast();

            // update times
            times.AddFirst(Time.deltaTime);
            times.RemoveLast();
        }
    }

    #endregion

    public Vector3 Velocity(JointType joint)
    {
        //var average = Vector3.zero;
        var diff = lastNpositions[joint].First() - lastNpositions[joint].Last();
        //foreach (var point in lastNpositions[joint])
        //{
        //    average += point;
        //}
        //var averagePosition = average / lastNpositions[joint].Count;
        //return averagePosition / times.Sum();
        return diff / times.Sum();
    }

    private static Vector3 ProjectJointPosition(Windows.Kinect.Joint joint, float z = 0f)
    {
        // TODO: do projection space calculations here instead of 10x values
        return new Vector3(joint.Position.X, joint.Position.Y * 10, z);
    }
}
