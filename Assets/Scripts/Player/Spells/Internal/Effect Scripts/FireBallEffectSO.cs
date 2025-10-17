using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/FireBall")]
public class FireBallEffectSO : EffectSO
{
    MonoBehaviour _cRunner;
    Vector3 _spawnPos;
    SpellSO _self;
    Mana _mana;
    float _cd;

    public override void Init(CastContext castContext)
    {
        _mana = castContext.Mana;
        _self = castContext.Spell;
        _cd = castContext.Spell.cooldown;
        _cRunner = castContext.CoroutineRunner;
        _spawnPos = castContext.SpawnPoint.position;
        castContext.Spell.canCast = false;

        FlameEffectSO.OnFlameSwitchOff += SwitchOff;
        FlameEffectSO.OnFlameSwitchOn += SwitchOn;
    }

    public override void OnCast()
    {
        
        var fireBall = FireBallFactory.Instance.GetFireBall();
        fireBall.transform.SetPositionAndRotation(_spawnPos, Camera.main.transform.rotation);
        _mana.Spend(_self.manaCost);

        _cRunner.StartCoroutine(Cooldown());
    }

    void SwitchOff() => _self.canCast = false;
    void SwitchOn() => _self.canCast = true;

    public override void Dispose()
    {
        FlameEffectSO.OnFlameSwitchOff -= SwitchOff;
        FlameEffectSO.OnFlameSwitchOn -= SwitchOn;
    }

    IEnumerator Cooldown()
    {
        _self.onCD = true;

        yield return new WaitForSeconds(_cd);

        _self.onCD = false;
    }
}
