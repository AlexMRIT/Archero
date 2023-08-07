using UnityEngine;

[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(GeneratorLocation))]
internal sealed class Initialization : MonoBehaviour
{
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private Player _player;

    private IModuleInit _generatorLocation;
    private IModuleInit _spawner;

    private void Awake()
    {
        _generatorLocation = GetComponent<GeneratorLocation>();
        _spawner = GetComponent<Spawner>();
    }

    private void Start()
    {
        if (!_generatorLocation.Init(null))
            return;

        object[] args = new object[1]
        {
            ((GeneratorLocation)_generatorLocation).GetLocation()
        };

        if (!_spawner.Init(args))
            return;

        args = new object[1]
        {
            (GeneratorLocation)_generatorLocation
        };

        if (!_camera.Init(args))
            return;

        PlayerHookEntity playerHook = _player.GetComponent<PlayerHookEntity>();

        args = new object[1]
        {
            (Spawner)_spawner
        };

        if (!playerHook.Init(args))
            return;

        _player.SetHookEntity(playerHook);
    }
}