using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spell Effects/Mana Spend")]
public class ManaSpendEffect : EffectSO
{
    [Tooltip("If active, the mana will drain over time instead of one time.")]
    [SerializeField] private bool _isDrain;
    private Mana _m;
    private float _c;

    public override void Init(CastContext castContext)
    {
        _c = castContext.Spell.manaCost;
        _m = castContext.Mana;
    }

    public override void OnCast()
    {
        if (_isDrain) _m.Drain(_c);
        else _m.Spend(_c);
        // Soy re capo
    }
}
