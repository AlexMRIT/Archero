using UnityEngine;

[RequireComponent(typeof(EntityAnimation))]
[RequireComponent(typeof(CharacterController))]
public abstract class Entity : MonoBehaviour
{
    public EntitySpecification EntitySpec;

    protected CharacterController EntityCharacterController;
    protected EntityAnimation EntityAnimationController;

    public virtual void TakeDamage(float damage, Entity attacker)
    {
        EntitySpec.Health -= damage;

        if (!EntitySpec.ValidAliveEntity())
            DeathEvent(attacker);
    }

    public virtual void Move(Vector3 direction)
    {
        EntityCharacterController.Move(direction);
    }

    protected abstract void DeathEvent(Entity attacker);
    public abstract void Spawn(Location location);
}