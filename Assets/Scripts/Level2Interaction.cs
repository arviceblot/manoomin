using UnityEngine;
using System.Collections;
using System.Linq;
using Windows.Kinect;

public class Level2Interaction : MonoBehaviour
{
    [SerializeField]
    private float force = 1f;

    private KinectBodyManager bodyManager;
    private ParchingRice[] rice;

    // Use this for initialization
    private void Start()
    {
        bodyManager = FindObjectOfType<KinectBodyManager>();

        rice = FindObjectsOfType<ParchingRice>();
    }

    // Update is called once per frame
    private void Update()
    {
        // get average hand velocities
        var average = Vector3.zero;
        foreach (var body in bodyManager.Bodies)
        {
            average += body.Velocity(JointType.HandLeft);
            average += body.Velocity(JointType.HandRight);
        }
        if (average != Vector3.zero)
        {
            average /= bodyManager.Bodies.Count * 2f;
        }

        // move the rice in the average velocity
        foreach (var grain in rice)
        {
            var rb = grain.GetComponent<Rigidbody2D>();
            var amount = Vector2.ClampMagnitude(new Vector2(average.x, average.y) * force, 16);
            amount.y = Mathf.Abs(amount.y); // only up force, not down
            rb.AddForce(Vector3.ClampMagnitude(average * force, 10));
        }
    }
}
