using UnityEngine;
using System.Collections;
using System.Linq;
using Windows.Kinect;

public class Level2Interaction : LevelInteration
{
    [SerializeField]
    private float force = 1f;
    [SerializeField]
    private ParticleSystem rice;
    [SerializeField]
    private ParticleSystem parchedRice;
    [SerializeField]
    private WindZone wind;
    [SerializeField]
    private float parchedSpawnTimeThreshold = 5;
    [SerializeField]
    private float notStirringTimeThreshold = 2;

    private ParticleSystem.ExternalForcesModule riceWind;
    private ParticleSystem.EmissionModule parchedEmmision;

    private float stirringTime = 0f;
    private float notStirringTime = 0f;

    //private ParchingRice[] rice;

    public override void Start()
    {
        base.Start();

        //rice = FindObjectsOfType<ParchingRice>();

        riceWind = rice.externalForces;
        parchedEmmision = parchedRice.emission;
    }

    private void Update()
    {
        // get average hand velocities
        var average = BodyManager.GetAverageVelocity(new JointType[] { JointType.HandLeft, JointType.HandRight });

        if (average.magnitude > 5)
        {
            // rotate the wind around it's own X axis
            var direction = average.normalized.x;
            wind.transform.Rotate(Vector3.right * direction * Time.deltaTime * 10000);

            /*
        }

        var average = new Vector2();

        foreach (var body in BodyManager.Bodies)
        {
            var directionLeft = body.Position(JointType.SpineMid) - body.Position(JointType.HandLeft);
            var directionRight = body.Position(JointType.SpineMid) - body.Position(JointType.HandRight);

            average += directionLeft + directionRight;
        }

        average /= BodyManager.Bodies.Count * 2f;

        var amount = Vector2.ClampMagnitude(new Vector2(average.x, average.y) * force, 16);
        amount.y = Mathf.Abs(amount.y); // only up force, not down
        amount = Vector2.ClampMagnitude(amount * force, 10);

        //if (amount.magnitude > 5f)
        if (average.magnitude > 2f)
        {
        */
            // we are stirring!
            //wind.transform.rotation = Quaternion.LookRotation(average);
            notStirringTime = 0f;
            stirringTime += Time.deltaTime;
            //wind.transform.rotation = Quaternion.Lerp(wind.transform.rotation, Quaternion.LookRotation(average * -1), Time.deltaTime * 6f);
            riceWind.multiplier = Mathf.Lerp(riceWind.multiplier, 1, 1);

            if (stirringTime > parchedSpawnTimeThreshold)
            {
                parchedEmmision.rateOverTime = Mathf.Lerp(parchedEmmision.rateOverTime.constant, 5, 10);
            }

            // audio
            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 1, Time.deltaTime);
        }
        else
        {
            notStirringTime += Time.deltaTime;

            if (notStirringTime > notStirringTimeThreshold)
            {
                stirringTime = 0f;
            }

            riceWind = rice.externalForces;
            riceWind.multiplier = Mathf.Lerp(riceWind.multiplier, 0, 1);
            parchedEmmision.rateOverTime = Mathf.Lerp(parchedEmmision.rateOverTime.constant, 0, 5);

            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 0, Time.deltaTime);
        }


        // move the rice in the average velocity
        /*
        foreach (var grain in rice)
        {
            var rb = grain.GetComponent<Rigidbody2D>();
            var amount = Vector2.ClampMagnitude(new Vector2(average.x, average.y) * force, 16);
            amount.y = Mathf.Abs(amount.y); // only up force, not down
            rb.AddForce(Vector3.ClampMagnitude(average * force, 10));
        }
        */
    }
}
