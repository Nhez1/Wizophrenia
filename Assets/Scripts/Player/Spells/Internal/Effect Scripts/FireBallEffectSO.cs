using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/FireBall")]
public class FireBallEffectSO : EffectSO
{
    public float cooldown;
    CastContext _context;
    bool _canShoot;

    public override void Init(CastContext castContext)
    {
        _context = castContext;
        _context.Spell.canCast = false;
        _canShoot = true;
        FlameEffectSO.OnFlameSwitchOff += SwitchOff;
        FlameEffectSO.OnFlameSwitchOn += SwitchOn;
    }

    public override void OnCast()
    {
        if (_canShoot)
        {
            var fireBall = FireBallFactory.Instance.GetFireBall();
            fireBall.transform.SetPositionAndRotation(_context.SpawnPoint.position, Camera.main.transform.rotation);
            _context.CoroutineRunner.StartCoroutine(Cooldown());
        }
        else return;
    }

    void SwitchOff() => _context.Spell.canCast = false;
    void SwitchOn() => _context.Spell.canCast = true;

    public override void Dispose()
    {
        FlameEffectSO.OnFlameSwitchOff -= SwitchOff;
        FlameEffectSO.OnFlameSwitchOn -= SwitchOn;
    }

    IEnumerator Cooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }
}
