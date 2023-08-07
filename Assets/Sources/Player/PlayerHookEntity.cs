using UnityEngine;

public sealed class PlayerHookEntity : HookEntity, IModuleInit
{
    private Spawner _spawner;

    public bool Init(object[] args)
    {
        if (args == null || args.Length == 0)
        {
            Debug.LogError($"Arguments is null or length 0.");
            return false;
        }

        _spawner = (Spawner)args[0];

        return true;
    }

    public bool TryHook(out Entity entity)
    {
        float minDistance = float.MaxValue;
        Entity closetsTarget = null;

        foreach (Entity ptrEntity in _spawner.GetEntities())
        {
            if (TryFindUnhiddenTarget(ptrEntity.transform, out float distance))
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closetsTarget = ptrEntity;
                }
            }
        }

        if (closetsTarget == null)
        {
            entity = null;
            return false;
        }
        else
        {
            entity = closetsTarget;
            return true;
        }
    }
}