using UnityEngine;
using System;

public class Timer
{

    private float lastTick;
    private readonly float interval;

    public static float getTime()
    {
        return Time.realtimeSinceStartup;
    }

    public Timer(float interval)
    {
        this.interval = interval;
        reset();
    }

    public bool hasElapsed()
    {
        return Time.realtimeSinceStartup - lastTick > interval;
    }

    public void reset()
    {
        lastTick = Time.realtimeSinceStartup;
        //Debug.Log("LT: " + lastTick);
    }

    public float getTimeLeft()
    {
        return interval - (Time.realtimeSinceStartup - lastTick);
    }

}
