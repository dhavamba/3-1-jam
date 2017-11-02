using UnityEngine;
using System;

public static class ExtendVector2
{

	public static Vector2 Abs (Vector2 vector)
    {
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
	}

    public static Vector2 Clamp(Vector2 vector, Vector2 sizeX, Vector2 sizeY)
    {
        vector.x = Mathf.Clamp(vector.x, sizeX.x, sizeX.y);
        vector.y = Mathf.Clamp(vector.y, sizeY.x, sizeY.y);
        return vector;
    }

    public static Vector2 Clamp(Vector2 vector, Vector2 size)
    {
        vector.x = Mathf.Clamp(vector.x, 0, size.x);
        vector.y = Mathf.Clamp(vector.y, 0, size.y);
        return vector;
    }

    public static bool IsInterval(Vector2 intern, Vector2 rangeX, Vector2 rangeY)
    {
        return intern.x >= rangeX.x && intern.x <= rangeX.y && intern.y >= rangeY.x && intern.y <= rangeY.y;
    }

    public static bool IsInterval(Vector2 intern, Vector2 range)
    {
        return IsInterval(intern, new Vector2(0, range.x), new Vector2(0, range.y));
    }

    public static float ScaleTransposed(Vector2 a, Vector2 b)
    {
        Vector2 c = Vector2.Scale(a, b);
        return c.x + c.y;
    }

    public static float? AngularCoefficient(Vector2 a)
    {
        if (a.x == 0)
        {
            return null;
        }
        return a.y / a.x;
    }

    public static Vector2 OrthogonalVector(Vector2 a)
    {
        return ExtendVector2.ReturnDirToAngle(ExtendVector2.ReturnAngleToDir(a) + 90);
    }

    public static bool IsParallel(Vector2 a, Vector2 b)
    {
        float c = a.x * b.y - a.y * b.x;
        return c == 0;
    }

    public static Vector2 ReturnDirToAngle(float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        return dir.normalized;
    }

    public static float ReturnAngleToDir(Vector2 dir)
    {
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle <= 0)
        {
            angle += 360;
        }
        return angle;
    }

    public static bool IsOrthogonal(Vector2 a, Vector2 b)
    {
        return Vector2.Dot(a, b) == 0;
    }


    public static float Angle(Vector2 a, Vector2 b)
    {
        float c = Vector2.Angle(a.normalized, b.normalized);
        if (a.y < 0)
        {
            c = 180 + (180 - c);
        }
        return c;
    }

    public static Vector2 CalcolateVectorWithAngleInSquare(Vector2 center, Vector2 dir, Vector2 sizeCollider, Vector2 sizeTransform)
    {
        float angle = ReturnAngleToDir(dir);
        float distance = 0;
        float newAngle = 0;
        Vector2 aux = Vector2.zero;

        if (angle >= 0 && angle <= 45)
        {
            newAngle = angle - 0;
            distance = Mathf.Tan(newAngle * Mathf.Deg2Rad);
            aux = new Vector2(1, distance);
        }
        else if (angle >= 45 && angle <= 90)
        {
            newAngle = angle - 45;
            distance = 1 - Mathf.Tan(newAngle * Mathf.Deg2Rad);
            aux = new Vector2(distance, 1);
        }
        else if (angle >= 90 && angle <= 135)
        {
            newAngle = angle - 90;
            distance = Mathf.Tan(newAngle * Mathf.Deg2Rad);
            aux = new Vector2(-distance, 1);
        }
        else if (angle >= 135 && angle <= 180)
        {
            newAngle = angle - 135;
            distance = 1 - Mathf.Tan(newAngle * Mathf.Deg2Rad);
            aux = new Vector2(-1, distance);
        }
        else if (angle >= 180 && angle <= 225)
        {
            newAngle = angle - 180;
            distance = Mathf.Tan(newAngle * Mathf.Deg2Rad);
            aux = new Vector2(-1, -distance);
        }
        else if (angle >= 225 && angle <= 270)
        {
            newAngle = angle - 225;
            distance = 1 - Mathf.Tan(newAngle * Mathf.Deg2Rad);
            aux = new Vector2(-distance, -1);
        }
        else if (angle >= 270 && angle <= 315)
        {
            newAngle = angle - 270;
            distance = Mathf.Tan(newAngle * Mathf.Deg2Rad);
            aux = new Vector2(distance, -1);
        }
        else if (angle >= 315)
        {
            newAngle = angle - 315;
            distance = 1 - Mathf.Tan(newAngle * Mathf.Deg2Rad);
            aux = new Vector2(1, -distance);
        }

        aux = Vector2.Scale(aux, sizeTransform);
        aux = Vector2.Scale(aux, sizeCollider * 0.5f);
        aux = center + aux;
        return aux;
    }
}
