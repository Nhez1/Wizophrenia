using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Cooldown")]
public class CooldownEffect : EffectSO
{
    SpellSO _spell;
    float _cd;

    MonoBehaviour _cRunner;

    public override void Init(CastContext castContext = null)
    {
        _cRunner = castContext.CoroutineRunner;
        _cd = castContext.Spell.cooldown;
        _spell = castContext.Spell;
    }

    public override void OnCast() => _cRunner.StartCoroutine(Cooldown());

    IEnumerator Cooldown()
    {
        _spell.onCD = true;

        yield return new WaitForSeconds(_cd);

        _spell.onCD = false;
    }
}
