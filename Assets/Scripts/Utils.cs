using UnityEngine;

public static class Utils
{
    public static float CountDownTimer(float timer)
    {
        return Mathf.Max(timer - Time.deltaTime, 0);
    }
}