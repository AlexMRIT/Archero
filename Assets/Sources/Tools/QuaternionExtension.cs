using UnityEngine;

public static class QuaternionExtension
{
    public static Quaternion RotateWithDirection(Vector3 direction)
    {
        return Quaternion.Euler(new Vector3(0.0f, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0.0f));
    }
}