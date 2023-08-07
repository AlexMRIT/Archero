using System;

[Serializable]
public struct EntitySpecification
{
    public float Health;
    public float MaxHealth;
    public float MovementSpeed;
    public float AttackSpeed;
    public float Damage;

    public bool ValidAliveEntity()
    {
        return Health > 0;
    }
}