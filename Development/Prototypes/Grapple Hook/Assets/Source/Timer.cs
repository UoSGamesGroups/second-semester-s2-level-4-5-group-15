using UnityEngine;

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
        lastTick = Time.realtimeSinceStartup;
    }

    public bool hasElapsed()
    {
        return Time.realtimeSinceStartup - lastTick > interval;
    }

    public void reset()
    {
        lastTick = Time.realtimeSinceStartup;
    }

}
