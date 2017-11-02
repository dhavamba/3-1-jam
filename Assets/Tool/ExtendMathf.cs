using UnityEngine;

public static class ExtendMathf
{
    private const int maxAngle = 360;

    //vede se due numeri sono simili tramite un fattore di precisione
    public static bool IsSimilar(float a, float b, float precision = 0.01f)
    {
        float difference = Mathf.Abs(a - b);
        return difference < precision;
    }

    public static bool IsInterval(float number, Vector2 range)
    {
        return number >= range.x && number <= range.y;
    }

    public static bool IsInterval(float number, float max)
    {
        return IsInterval(number, new Vector2(0, max));
    }

    public static float ChangeRange(this float value, Vector2 originalRange, Vector2 newRange)
    {
        float scale = (float)(newRange.y - newRange.x) / (originalRange.y - originalRange.x);
        return (float)(newRange.x + ((value - originalRange.x) * scale));
    }

    public static float ChangeRange(this float value, float oldMax, float newMax)
    {
        return ChangeRange(value, new Vector2(0, oldMax), new Vector2(0, newMax));
    }

    public static float Percent(this float value, float oldMax)
    {
        return ChangeRange(value, new Vector2(0, oldMax), new Vector2(0, 1));
    }

    public static float Percent(this float value, Vector2 originalRange)
    {
        return ChangeRange(value, new Vector2(originalRange.x, originalRange.y), new Vector2(0, 1));
    }

    public static int AddInCircleRange(this int value, int max)
    {
        return AddInCirlceRange(value, new UnityEngine.Vector2(0, max));
    }

    public static int AddInCirlceRange(this int value, UnityEngine.Vector2 range)
    {
        value++;
        if (value > range.y)
        {
            value = (int)range.x;
        }
        return value;
    }
}
