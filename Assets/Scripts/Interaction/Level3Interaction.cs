﻿using UnityEngine;
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

    public override void Start()
    {
        base.Start();

        if (ApplicationManager.Instance.UseDebugeMode)
        {
            // enable debug elements
            slider.enabled = true;
            label.enabled = true;
        }

        slider.maxValue = 100;
        slider.minValue = 0;
        slider.value = slider.minValue;
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

        slider.value += increase;
        label.text = slider.value.ToString();
    }
}