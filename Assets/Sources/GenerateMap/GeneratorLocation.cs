using UnityEngine;

[RequireComponent(typeof(Location))]
internal sealed class GeneratorLocation : MonoBehaviour, IModuleInit
{
    [SerializeField] private float _lengthPlane;
    [SerializeField] private Material _planeMaterial;
    [SerializeField] private float _movePlaneForward;
    [SerializeField] private Vector2 _sizeLocation;
    [SerializeField] private float _spacingStone;
    [SerializeField] private float _powerFlowStones;

    private const float NumberСubesPerUnit = 10.0f;
    private GameObject[,] _location;
    private Location _buildLocation;

    public bool Init(object[] args)
    {
        if (_lengthPlane <= 0)
        {
            Debug.LogError($"The length of the plane must not be less than or equal to 0.");
            return false;
        }

        if ((_sizeLocation.x / NumberСubesPerUnit) * _spacingStone > 1.0f ||
            (_sizeLocation.y / NumberСubesPerUnit) * _spacingStone > _lengthPlane)
        {
            Debug.LogError("The length of the wall exceeds the length of the location.");
            return false;
        }

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        plane.GetComponent<MeshRenderer>().material = _planeMaterial;
        plane.transform.SetPositionAndRotation(new Vector3(0.0f, 0.0f, _movePlaneForward), Quaternion.identity);
        plane.transform.localScale = new Vector3(1.0f, 1.0f, _lengthPlane);

        int countCubeX = (int)_sizeLocation.x;
        int countCubeY = (int)_sizeLocation.y;

        _location = new GameObject[countCubeX, countCubeY];

        for (int iterY = 0; iterY < countCubeY; iterY++)
        {
            if (iterY == 0 || iterY == countCubeY - 1)
            {
                for (int iterX = 0; iterX < countCubeX; iterX++)
                    _location[iterX, iterY] = InternalCreateCube(plane, iterX, iterY);
            }
            else
            {
                for (int iterX = 0; iterX < countCubeX; iterX++)
                {
                    if (iterX != 0 && iterX != countCubeX - 1)
                        continue;

                    _location[iterX, iterY] = InternalCreateCube(plane, iterX, iterY);
                }
            }

            for (int iterX = 0; iterX < countCubeX; iterX++)
            {
                if (iterY == 0 || iterX == 0 || iterY == countCubeY - 1 || iterX == countCubeX - 1)
                    continue;

                if (InternalTryGetRandom())
                    _location[iterX, iterY] = InternalCreateCube(plane, iterX, iterY);
            }
        }

        Location location = GetComponent<Location>();
        location.SetLocation(_location, _sizeLocation, plane, _lengthPlane, _spacingStone);
        _buildLocation = location;

        return true;
    }

    private GameObject InternalCreateCube(GameObject plane, int iterX, int iterY)
    {
        GameObject stone = GameObject.CreatePrimitive(PrimitiveType.Cube);

        stone.layer = LayerMask.NameToLayer("Stone");

        float posX = ((plane.transform.position.x + 1) * iterX) - _lengthPlane - 1.25f + (_spacingStone * iterX);
        float posY = plane.transform.position.y + (transform.localScale.y / 2.0f);
        float posZ = (plane.transform.position.z * _lengthPlane - iterY) - (_spacingStone * iterY);

        stone.transform.position = new Vector3(posX, posY, posZ);

        return stone;
    }

    private bool InternalTryGetRandom()
    {
        return Random.Range(0, 100) < _powerFlowStones;
    }

    public void ClampLocationCamera(Transform cameraTransform)
    {
        cameraTransform.position = new Vector3(cameraTransform.position.x,
            cameraTransform.position.y, Mathf.Clamp(cameraTransform.position.z, 1, _movePlaneForward));
    }

    public Location GetLocation()
    {
        return _buildLocation;
    }
}