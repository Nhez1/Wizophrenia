using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "ScriptableObjects/Items/Potion")]
public class PotionSO : ItemSO
{
    [Tooltip("Insert a positive number to heal and a negative number to damage.")]
    public float healthModifier;
    [Tooltip("Insert a positive number to regenerate and a negative number to deplete.")]
    public float manaModifier;
    [Tooltip("Insert a positive number to regenerate and a negative number to deplete.")]
    public float sanityModifier;

    public void Consume(PotionContext potionContext)
    {
        // Health
        if (healthModifier > 0) potionContext.life.Heal(healthModifier);
        if (healthModifier < 0) potionContext.life.Damage(healthModifier);

        // Mana
        if (manaModifier < 0) potionContext.mana.Restore(manaModifier);
        if (manaModifier > 0) potionContext.mana.Spend(manaModifier);

        // Sanity
        if (sanityModifier < 0) potionContext.sanity.Heal(sanityModifier);
        if (sanityModifier > 0) potionContext.sanity.Reduce(sanityModifier);
    }
}

public class PotionContext
{
    public Life life;
    public Mana mana;
    public Sanity sanity;
    //Acá también se va a pedir el script de Sanidad, cuando esté hecho.

    public PotionContext(Life L, Mana M, Sanity S)
    {
        life = L;
        mana = M;
        sanity = S;
    }
}
