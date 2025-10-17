using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/FireBall")]
public class FireBallEffectSO : EffectSO
{
    CastContext _context;
    SpellSO _self;

    public override void Init(CastContext castContext)
    {
        _context = castContext;
        _self = castContext.Spell;
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
    }

    void SwitchOff() => _self.canCast = false;
    void SwitchOn() => _self.canCast = true;

    public override void Dispose()
    {
        ShadowHand.ForceFlameOff -= SwitchOff;
        FlameEffectSO.OnFlameSwitchOff -= SwitchOff;
        FlameEffectSO.OnFlameSwitchOn -= SwitchOn;
    }
}
