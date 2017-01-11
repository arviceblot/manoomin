using UnityEngine;

public class LevelInteration : MonoBehaviour
{
    [SerializeField]
    private KeyCode manualTrigger = KeyCode.Space;

    private ApplicationManager appManager;

    private KinectBodyManager bodyManager;

    public ApplicationManager AppManager
    {
        get { return this.appManager; }
    }

    public KinectBodyManager BodyManager
    {
        get { return this.bodyManager; }
    }

    public virtual void Start()
    {
        bodyManager = FindObjectOfType<KinectBodyManager>();
        appManager = FindObjectOfType<ApplicationManager>();
    }
}
