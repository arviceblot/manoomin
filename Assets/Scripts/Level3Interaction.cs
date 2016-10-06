using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Windows.Kinect;

public class Level3Interaction : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text label;
    [SerializeField]
    private float fallSpeed = 10f;

    private KinectBodyManager bodyManager;

    // Use this for initialization
    void Start()
    {
        slider.maxValue = 100;
        slider.minValue = 0;
        slider.value = slider.minValue;

        bodyManager = FindObjectOfType<KinectBodyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // decrease slider by update amount
        slider.value -= fallSpeed * Time.deltaTime;

        // get the positive Y velocities of everyone
        float increase = 0f;
        foreach (var body in bodyManager.Bodies)
        {
            var amount = Mathf.Clamp(body.Velocity(JointType.FootLeft).y, 0, 20);
            amount += Mathf.Clamp(body.Velocity(JointType.FootRight).y, 0, 20);
            increase += amount / 2f;
        }
        slider.value += increase;
        label.text = slider.value.ToString();
    }
}
