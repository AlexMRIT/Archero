using UnityEngine;

[RequireComponent(typeof(Camera))]
internal sealed class CameraMovement : MonoBehaviour, IModuleInit
{
    [SerializeField] private Entity _target;

    private GeneratorLocation _generatorLocation;
    private float _orthographicSize;

    public bool Init(object[] args)
    {
        if (args == null || args.Length == 0)
        {
            Debug.LogError($"Arguments is null or length 0.");
            return false;
        }

        GeneratorLocation generator = (GeneratorLocation)args[0];
        _generatorLocation = generator;

        Camera camera = GetComponent<Camera>();
        _orthographicSize = camera.orthographicSize;

        return true;
    }

    private void Update()
    {
        _generatorLocation.ClampLocationCamera(transform);

        const float time = 0.1f;

        if ((_target.transform.position.z - 1.0f) > _orthographicSize / 3f || transform.position.z > 1)
            transform.position = new Vector3(transform.position.x, transform.position.y,
                transform.position.z + (_target.transform.position.z - transform.position.z) * time);
    }
}