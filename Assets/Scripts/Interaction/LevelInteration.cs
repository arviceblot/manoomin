using UnityEngine;

public class LevelInteration : MonoBehaviour
{
    [SerializeField]
    private KeyCode manualTrigger = KeyCode.Space;

    [SerializeField]
    private AudioSource soundEffect;

    private ApplicationManager appManager;

    private KinectBodyManager bodyManager;

    private float startTime;

    public AudioSource SoundEffect
    {
        get { return soundEffect; }
    }

    public ApplicationManager AppManager
    {
        get { return this.appManager; }
    }

    public KinectBodyManager BodyManager
    {
        get { return this.bodyManager; }
    }

    public float PlayTime
    {
        get
        {
            return Time.time - startTime;
        }
    }

    public virtual void Start()
    {
        bodyManager = FindObjectOfType<KinectBodyManager>();
        appManager = FindObjectOfType<ApplicationManager>();

        // start audio effects at 0
        if (soundEffect != null)
        {
            soundEffect.volume = 0;
        }

        startTime = Time.time;
    }
}
