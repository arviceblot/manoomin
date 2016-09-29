using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Collections.Generic;

public class KinectBody
{
    private static int POSITIONS = 5;

    private IDictionary<Windows.Kinect.JointType, Vector3> positions;
    private IDictionary<Windows.Kinect.JointType, IList<Vector3>> lastNpositions;

    public KinectBody()
    {
        positions = new Dictionary<JointType, Vector3>();

        lastNpositions = new Dictionary<JointType, IList<Vector3>>();
        for (JointType joint = JointType.SpineBase; joint <= JointType.ThumbRight; joint++)
        {
            lastNpositions[joint] = new List<Vector3>();
        }
    }

    #region MonoBehaviour

    void Start()
    {

    }

    void Update()
    {

    }

    #endregion

    public void Update(Body body)
    {
        // update positions of all parts
        for (JointType joint = JointType.SpineBase; joint <= JointType.ThumbRight; joint++)
        {
            positions[joint] = ProjectJointPosition(body.Joints[joint]);
        }
    }

    public Vector3 Velocity(JointType joint)
    {
        var average = Vector3.zero;
        foreach (var point in lastNpositions[joint])
        {
            average += point;
        }
        return average / lastNpositions[joint].Count;
    }

    private static Vector3 ProjectJointPosition(Windows.Kinect.Joint joint, float z = 0f)
    {
        // TODO: do projection space calculations here instead of 10x values
        return new Vector3(joint.Position.X, joint.Position.Y * 10, z);
    }
}
