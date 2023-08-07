using UnityEngine;

public sealed class Location : MonoBehaviour
{
    private GameObject[,] _location;
    private Vector2 _sizeLocation;
    private GameObject _plane;
    private float _lengthPlane;
    private float _spacing;

    public void SetLocation(GameObject[,] location,
        Vector2 size, GameObject plane, float lengthPlane, float spacing)
    {
        _location = location;
        _sizeLocation = size;
        _plane = plane;
        _lengthPlane = lengthPlane;
        _spacing = spacing;
    }

    public GameObject[,] GetLocation()
    {
        return _location;
    }

    public Vector2 GetSizeLocation()
    {
        return _sizeLocation;
    }

    public GameObject GetPlaneLocation()
    {
        return _plane;
    }

    public float GetLengthPlane()
    {
        return _lengthPlane;
    }

    public float GetSpacing()
    {
        return _spacing;
    }
}