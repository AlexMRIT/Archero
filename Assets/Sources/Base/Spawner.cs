using UnityEngine;

public sealed class Spawner : MonoBehaviour, IModuleInit
{
    [SerializeField] private Entity _player;
    [SerializeField] private Entity[] _enemies;

    private Location _location;

    public bool Init(object[] args)
    {
        if (args == null || args.Length == 0)
        {
            Debug.LogError($"Arguments is null or length 0.");
            return false;
        }

        _location = (Location)args[0];
        _player.Spawn(_location);

        for (int iterator = 0; iterator < _enemies.Length; iterator++)
            _enemies[iterator].Spawn(_location);

        return true;
    }

    public Entity[] GetEntities()
    {
        return _enemies;
    }

    public Entity GetPlayer()
    {
        return _player;
    }
}