using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/FireBall")]
public class FireBallEffectSO : EffectSO
{
    MonoBehaviour _cRunner;
    CastContext _context;
    SpellSO _self;
    Mana _mana;

    public override void Init(CastContext castContext)
    {
        _context = castContext;
        _mana = castContext.Mana;
        _self = castContext.Spell;
        _cRunner = castContext.CoroutineRunner;
        castContext.Spell.canCast = false;

        ShadowHand.ForceFlameOff += SwitchOff;
        FlameEffectSO.OnFlameSwitchOff += SwitchOff;
        FlameEffectSO.OnFlameSwitchOn += SwitchOn;
    }

    public override void OnCast()
    {
        var spawnPos = _context.SpawnPoint.position;
        
        var fireBall = FireBallFactory.Instance.GetFireBall();
        fireBall.transform.SetPositionAndRotation(spawnPos, Camera.main.transform.rotation);
        _mana.Spend(_self.manaCost);

        _cRunner.StartCoroutine(Cooldown());
    }

    void SwitchOff() => _self.canCast = false;
    void SwitchOn() => _self.canCast = true;

    public override void Dispose()
    {
        ShadowHand.ForceFlameOff -= SwitchOff;
        FlameEffectSO.OnFlameSwitchOff -= SwitchOff;
        FlameEffectSO.OnFlameSwitchOn -= SwitchOn;
    }

    IEnumerator Cooldown()
    {
        _self.onCD = true;

        yield return new WaitForSeconds(_self.cooldown);

        _self.onCD = false;
    }
}
