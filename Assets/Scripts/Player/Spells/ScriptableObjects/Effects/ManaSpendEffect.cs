using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/ManaSpend")]
public class ManaSpendEffect : EffectSO
{
    [Tooltip("If active, the mana will drain over time instead of one time.")]
    public bool isDrain;
    Mana m;
    float c;

    public override void OnCast(CastContext castContext)
    {
        c = castContext.Spell.manaCost;
        m = castContext.Mana;
        if (isDrain) m.Drain(c);
        else m.Spend(c);
        // Soy re capo
    }
}
