using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Cooldown")]
public class CooldownEffect : EffectSO
{
    SpellSO spell;
    float cooldown;

    public override void OnCast(CastContext castContext = null)
    {
        spell = castContext.Spell;
        cooldown = castContext.Spell.cooldown;

        castContext.CoroutineRunner.StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        spell.canCast = false;

        yield return new WaitForSeconds(cooldown);

        spell.canCast = true;
    }
}
