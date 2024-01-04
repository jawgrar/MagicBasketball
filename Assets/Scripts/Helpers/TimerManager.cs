using UnityEngine;

public static class TimerManager
{
    private static float timer;

    public static void Reset(float timerDuration)
    {
        timer = timerDuration;
    }

    public static float GetTimer()
    {
        timer -= Time.deltaTime;
        return timer;
    }

    public static bool IsTimeUp()
    {
        return timer <= 0;
    }
}
