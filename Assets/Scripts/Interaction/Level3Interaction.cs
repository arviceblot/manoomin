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
    private float maxNoiseSpeed = 3f;

    private ParticleSystem.NoiseModule dancingNoise;
    private float minNoiseSpeed;

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
        minNoiseSpeed = dancingNoise.scrollSpeed.constant;
    }

    private void FixedUpdate()
    {
        // decrease slider by update amount
        slider.value -= fallSpeed * Time.deltaTime;

        // get the positive Y velocities of everyone
        float increase = 0f;

        foreach (var body in BodyManager.Bodies)
        {
            var amount = Mathf.Clamp(body.Velocity(JointType.FootLeft).y, 0, 20);
            amount += Mathf.Clamp(body.Velocity(JointType.FootRight).y, 0, 20);
            increase += amount;
        }

        slider.value = Mathf.Clamp(slider.value + increase, slider.minValue, slider.maxValue);
        label.text = slider.value.ToString();

        dancingNoise.scrollSpeed = (maxNoiseSpeed / slider.maxValue) * slider.value;

        SoundEffect.volume = Mathf.Lerp(SoundEffect.volume, slider.value / slider.maxValue, Time.deltaTime);
    }
}
