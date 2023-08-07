using UnityEngine;

public abstract class ShellBase : MonoBehaviour
{
    protected void OnTriggerEnter(Collider collider)
    {
        if (!collider.TryGetComponent(out Entity entity))
        {
            OnStoneCollision(collider.transform);
            return;
        }

        OnEntityCollision(entity);
    }

    protected abstract void OnStoneCollision(Transform stone);
    protected abstract void OnEntityCollision(Entity entity);
}