using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Cooldown")]
public class CooldownEffect : EffectSO
{
    SpellSO spell;
    float cooldown;

    MonoBehaviour coroutineRunner;

    public override void Init(CastContext castContext = null)
    {
        spell = castContext.Spell;
        cooldown = castContext.Spell.cooldown;
        coroutineRunner = castContext.CoroutineRunner;
    }

    public override void OnCast() => coroutineRunner.StartCoroutine(Cooldown());

    IEnumerator Cooldown()
    {
        spell.canCast = false;

        yield return new WaitForSeconds(cooldown);

        spell.canCast = true;
    }
}
