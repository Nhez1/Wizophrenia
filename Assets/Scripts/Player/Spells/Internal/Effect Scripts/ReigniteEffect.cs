using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/ExpansiveWave")]
public class ReigniteEffect : EffectSO
{
    CastContext _context;
    SpellSO _self;

    public override void Init(CastContext castContext)
    {
        _self = castContext.Spell;
        _context = castContext;

        FlameEffectSO.OnFlameSwitchOn += SwitchOn;
        FlameEffectSO.OnFlameSwitchOff += SwitchOff;
        ShadowHand.ForceFlameOff += SwitchOff;
    }

    public override  void OnCast()
    {
        var spawnPoint = _context.SpawnPoint.position;
        var wave = ExpansiveWaveFactory.Instance.GetExpansiveWave();
        wave.transform.SetPositionAndRotation(spawnPoint, Camera.main.transform.rotation);
    }

    public override void Dispose()
    {
        FlameEffectSO.OnFlameSwitchOn -= SwitchOn;
        FlameEffectSO.OnFlameSwitchOff -= SwitchOff;
        ShadowHand.ForceFlameOff -= SwitchOff;
    }

    void SwitchOff() => _self.canCast = false;
    void SwitchOn() => _self.canCast = true;
}