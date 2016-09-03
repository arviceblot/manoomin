using UnityEngine;
using Windows.Kinect;
using System;
using System.Collections.Generic;

public class KinectBodyManager : MonoBehaviour
{
    public event Action<Body> OnBodyEnter;
    public event Action<ulong> OnBodyLeave;

    [SerializeField]
    private BodySourceManager bodySourceManager;

    private Dictionary<ulong, Body> bodies;

    // Use this for initialization
    void Start()
    {
        bodies = new Dictionary<ulong, Body>();
    }

    // Update is called once per frame
    void Update()
    {
        var data = bodySourceManager.GetData();

        // figure out if any bodies have been updated
        // also keep track of the current tracked IDs
        var trackedIDs = new List<ulong>();
        foreach (var body in data)
        {
            if (body.IsTracked)
            {
                trackedIDs.Add(body.TrackingId);
            }
        }

        // remove untracked bodies
        foreach (var id in bodies.Keys)
        {
            if (!trackedIDs.Contains(id))
            {
                BodyLeave(id);
                bodies.Remove(id);
            }
        }

        // add any new bodies
        foreach (var body in data)
        {
            if (body.IsTracked)
            {
                if (!bodies.ContainsKey(body.TrackingId))
                {
                    bodies[body.TrackingId] = body;
                    BodyEnter(body);
                }
            }
        }
    }

    private void BodyEnter(Body body)
    {
        if (OnBodyEnter != null)
        {
            OnBodyEnter(body);
        }
    }

    private void BodyLeave(ulong id)
    {
        if (OnBodyLeave != null)
        {
            OnBodyLeave(id);
        }
    }

    public Body GetBody(ulong id)
    {
        if (bodies.ContainsKey(id))
        {
            return bodies[id];
        }
        else
        {
            return null;
        }
    }

    public static Vector3 GetJointPosition2D(Windows.Kinect.Joint joint, float z = 0f)
    {
        // TODO: do projection space calculations here instead of 10x values
        return new Vector3(joint.Position.X, joint.Position.Y * 10, z);
    }
}
