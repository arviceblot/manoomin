using UnityEngine;
using Windows.Kinect;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// This class manages Kinecy body data and calculations.
/// </summary>
public class KinectBody : MonoBehaviour
{
    /// <summary>
    /// The number of position and time values we track for calculating velocity.
    /// </summary>
    private static int POSITIONS = 5;

    /// <summary>
    /// The unique Id associated with this body.
    /// </summary>
    private ulong id;
    /// <summary>
    /// The Kinect Body associated with this body.
    /// </summary>
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

    /// <summary>
    /// Gets the Id.
    /// </summary>
    public ulong Id
    {
        get { return id; }
    }

    /// <summary>
    /// Gets or sets the Kinect Body.
    /// </summary>
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
        foreach (JointType joint in Enum.GetValues(typeof(JointType)))
        {
            positions[joint] = new Vector3();
        }

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

    /// <summary>
    /// Calculates the velocity for a specific joint for this body.
    /// </summary>
    /// <param name="joint">
    /// The joint.
    /// </param>
    /// <returns>
    /// the velocity vector.
    /// </returns>
    public Vector3 Velocity(JointType joint)
    {
        if (lastNpositions == null)
        {
            return new Vector3();
        }

        var diff = lastNpositions[joint].First() - lastNpositions[joint].Last();

        return diff / times.Sum();
    }

    /// <summary>
    /// Gets the 2D position for a specific joint.
    /// </summary>
    /// <param name="joint">
    /// The joint.
    /// </param>
    /// <returns>
    /// The position as a 2D vector.
    /// </returns>
    public Vector2 Position(JointType joint)
    {
        return positions[joint];
    }

    /// <summary>
    /// Calculates the projected joint position in world space from Kinect space.
    /// </summary>
    /// <param name="joint">
    /// The joint.
    /// </param>
    /// <param name="z">
    /// The base z position.
    /// </param>
    /// <returns>
    /// The projected position.
    /// </returns>
    private static Vector3 ProjectJointPosition(Windows.Kinect.Joint joint, float z = 0f)
    {
        // TODO: do projection space calculations here instead of 10x values
        return new Vector3(joint.Position.X, joint.Position.Y * 10, z);
    }
}
