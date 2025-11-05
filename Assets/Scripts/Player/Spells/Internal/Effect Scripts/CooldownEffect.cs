using System.Collections;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Spell Effects/Cooldown")]
public class CooldownEffect : EffectSO
{
    public static event Action<SpellSO> OnCooldownStart;
    public static event Action<SpellSO> OnCooldownOver;

    private SpellSO _self;
    private float _cd;

    MonoBehaviour _cRunner;

    public override void Init(CastContext castContext)
    {
        _cRunner = castContext.CoroutineRunner;
        _cd = castContext.Spell.cooldown;
        _self = castContext.Spell;
    }

    public override void OnCast() => _cRunner.StartCoroutine(Cooldown());

    IEnumerator Cooldown()
    {
        OnCooldownStart?.Invoke(_self);
        _self.onCD = true;

        yield return new WaitForSeconds(_cd);

        OnCooldownOver?.Invoke(_self);
        _self.onCD = false;
    }
}
