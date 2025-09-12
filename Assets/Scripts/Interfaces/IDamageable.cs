public interface IDamageable
{
    float MaxHP { get; }
    float HP { get; }

    void TakeDamage(float amount);

    void TakeHeal(float amount);
}
