public interface IDamageable
{
    float maxHP { get; }
    float hp { get; }

    void TakeDamage(float amount);

    void TakeHeal(float amount);
}
