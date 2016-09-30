using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;
using System;
using System.Collections;
using System.Collections.Generic;

public class KinectBodyManager : MonoBehaviour
{
    public event Action<KinectBody> EventBodyEnter;
    public event Action<ulong> EventBodyLeave;

    [SerializeField]
    private BodySourceManager bodySourceManager;
    [SerializeField]
    private bool debug = false;
    [SerializeField]
    private Text debugText;

    private IDictionary<ulong, KinectBody> kinectBodies;

    public bool Debug
    {
        get { return debug; }
        set
        {
            debug = value;
            if (debug)
            {
                StartCoroutine(DisplayDebug());
            }
            else
            {
                StopCoroutine(DisplayDebug());
            }
        }
    }

    public ICollection<KinectBody> Bodies
    {
        get { return kinectBodies.Values; }
    }

    #region MonoBehaviour

    private void Start()
    {
        kinectBodies = new Dictionary<ulong, KinectBody>();
        Debug = debug;
    }

    private void Update()
    {
        var data = bodySourceManager.GetData();

        // this will usually not be initialzed at the start of play
        if (data == null)
        {
            return;
        }

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
                kinectBodies[id].enabled = false;
            }
        }

        // add any new bodies
        foreach (var body in data)
        {
            if (body.IsTracked)
            {
                if (!kinectBodies.ContainsKey(body.TrackingId))
                {
                    var newBody = gameObject.AddComponent<KinectBody>();
                    newBody.Body = body;
                    kinectBodies[body.TrackingId] = newBody;
                    OnBodyEnter(newBody);
                }
                else if (!kinectBodies[body.TrackingId].enabled)
                {
                    kinectBodies[body.TrackingId].enabled = true;
                }
            }
        }
    }

    #endregion

    #region Event Handlers

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

    #endregion

    private IEnumerator DisplayDebug()
    {
        while (true)
        {
            debugText.text = "Hand Velocity\n";
            foreach (var body in Bodies)
            {
                debugText.text += "Left: " + body.Velocity(Windows.Kinect.JointType.HandLeft) + "\t";
                debugText.text += "Right: " + body.Velocity(Windows.Kinect.JointType.HandRight) + "\n";
            }

            yield return null;
        }
    }
    public static Vector3 GetJointPosition2D(Windows.Kinect.Joint joint, float z = 0f)
    {
        // TODO: do projection space calculations here instead of 10x values
        return new Vector3(joint.Position.X, joint.Position.Y * 10, z);
    }
}
