using UnityEngine;

public static class ExtendQuaternion
{
    public static Quaternion Direction(Vector2 dir)
    {
        return Quaternion.AngleAxis(ExtendVector2.ReturnAngleToDir(dir), Vector3.forward);
    }
}
