using UnityEngine;

public static class FloatExtensions
{
    public static string ToFormattedTime(this float time)
    {
        string result;
        int seconds = Mathf.RoundToInt(time);
        result = seconds / 3600 + ":" + seconds / 60 + "." + seconds % 60;
        return result;
    }
}
