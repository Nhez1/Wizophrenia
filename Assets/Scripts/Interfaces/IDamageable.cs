using UnityEngine;

public interface IDamageable
{
    Life Life { get; }
}

public interface IKnockbackable
{
    void Knockback(Vector3 source, float force, float duration);
}