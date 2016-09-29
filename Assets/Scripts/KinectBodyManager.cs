using UnityEngine;
using Windows.Kinect;
using System;
using System.Collections.Generic;

public class KinectBodyManager : MonoBehaviour
{
    public event Action<KinectBody> EventBodyEnter;
    public event Action<ulong> EventBodyLeave;

    [SerializeField]
    private BodySourceManager bodySourceManager;

    private IDictionary<ulong, KinectBody> kinectBodies;

    public ICollection<KinectBody> Bodies
    {
        get { return kinectBodies.Values; }
    }

    #region MonoBehaviour

    private void Start()
    {
        kinectBodies = new Dictionary<ulong, KinectBody>();
    }

    private void Update()
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
        foreach (var id in kinectBodies.Keys)
        {
            if (!trackedIDs.Contains(id))
            {
                OnBodyLeave(id);
                kinectBodies.Remove(id);
            }
        }

        // add any new bodies
        foreach (var body in data)
        {
            if (body.IsTracked)
            {
                if (!kinectBodies.ContainsKey(body.TrackingId))
                {
                    var newBody = new KinectBody();
                    kinectBodies[body.TrackingId] = newBody; 
                    OnBodyEnter(newBody);
                }
            }
        }
        
        // update all existing bodies
        foreach (var body in data)
        {
            kinectBodies[body.TrackingId].Update(body);
        }
    }

    #endregion

    private void OnBodyEnter(KinectBody body)
    {
        if (EventBodyEnter != null)
        {
            EventBodyEnter(body);
        }
    }

    private void OnBodyLeave(ulong id)
    {
        if (EventBodyLeave != null)
        {
            EventBodyLeave(id);
        }
    }

    public KinectBody GetBody(ulong id)
    {
        if (kinectBodies.ContainsKey(id))
        {
            return kinectBodies[id];
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
