using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Windows.Kinect;

public class Level3Interaction : LevelInteration
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text label;
    [SerializeField]
    private float fallSpeed = 10f;
    [SerializeField]
    private ParticleSystem dancingRice;
    [SerializeField]
    private float maxNoiseSpeed = 0.5f;

    private ParticleSystem.NoiseModule dancingNoise;
    private float minNoiseSpeed = 0;

    public override void Start()
    {
        base.Start();

        if (ApplicationManager.Instance.UseDebugeMode)
        {
            // enable debug elements
            slider.gameObject.SetActive(true);
            label.enabled = true;
        }

        slider.maxValue = 100;
        slider.minValue = 0;
        slider.value = slider.minValue;

        dancingNoise = dancingRice.noise;
    }

    private void Update()
    {
        // decrease slider by update amount
        slider.value -= fallSpeed * Time.deltaTime;

        // get the positive Y velocities of everyone
        float increase = 0f;

        foreach (var body in BodyManager.Bodies)
        {
            var amount = Mathf.Clamp(Mathf.Max(body.Velocity(JointType.FootLeft).y, body.Velocity(JointType.FootRight).y), 0, 20);
            increase += amount;
        }
        
        if (increase > 5)
        {
            dancingNoise.strength = Mathf.Min(dancingNoise.strength.constant + Time.deltaTime, 1);
            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 1, Time.deltaTime);
        }
        else
        {
            dancingNoise.strength = Mathf.Max(dancingNoise.strength.constant - Time.deltaTime * 0.25f, 0);
            SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, 0, Time.deltaTime);
        }

        /*
        slider.value = Mathf.Clamp(slider.value + increase, slider.minValue, slider.maxValue);
        label.text = slider.value.ToString();

        dancingNoise.strength = (maxNoiseSpeed / slider.maxValue) * slider.value;

        SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, slider.value / slider.maxValue, Time.deltaTime);
        */
    }
}
