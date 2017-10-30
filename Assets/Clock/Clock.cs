using System;
using JetBrains.Annotations;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private const float
        DEGREES_PER_HOUR = 30f,
        DEGREES_PER_MINUTE = 6f,
        DEGREES_PER_SECOND = 6f;
    public bool Continuous;
    public Transform HoursTransform, MinutesTransform, SecondsTransform;

    [UsedImplicitly]
    private void UpdateContinuous()
    {
        var time = DateTime.Now.TimeOfDay;
        HoursTransform.localRotation =
            Quaternion.Euler(0f, (float) (time.TotalHours * DEGREES_PER_HOUR), 0f);
        MinutesTransform.localRotation =
            Quaternion.Euler(0f, (float) (time.TotalMinutes * DEGREES_PER_MINUTE), 0f);
        SecondsTransform.localRotation =
            Quaternion.Euler(0f, (float) (time.TotalSeconds * DEGREES_PER_SECOND), 0f);
    }

    private void Update()
    {
        if (Continuous) UpdateContinuous();
        else UpdateDiscrete();
    }

    [UsedImplicitly]
    private void UpdateDiscrete()
    {
        var time = DateTime.Now;
        HoursTransform.localRotation =
            Quaternion.Euler(0f, time.Hour * DEGREES_PER_HOUR, 0f);
        MinutesTransform.localRotation =
            Quaternion.Euler(0f, time.Minute * DEGREES_PER_MINUTE, 0f);
        SecondsTransform.localRotation =
            Quaternion.Euler(0f, time.Second * DEGREES_PER_SECOND, 0f);
    }
}