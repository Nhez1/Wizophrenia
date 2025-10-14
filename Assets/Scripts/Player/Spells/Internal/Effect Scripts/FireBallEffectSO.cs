using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/FireBall")]
public class FireBallEffectSO : EffectSO
{
    Transform spawnPoint;
    bool canShoot;

    public override void Init(CastContext castContext)
    {
        canShoot = false;
        spawnPoint = castContext.SpawnPoint;
        FlameEffectSO.OnFlameSwitchOff += SwitchOff;
        FlameEffectSO.OnFlameSwitchOn += SwitchOn;
    }

    public override void OnCast()
    {
        if (canShoot)
        {
            var fireBall = FireBallFactory.Instance.GetFireBall();
            fireBall.transform.SetPositionAndRotation(spawnPoint.position, Camera.main.transform.rotation);
        }
        else return;
    }

    void SwitchOff() => canShoot = false;
    void SwitchOn() => canShoot = true;

    public override void Dispose()
    {
        FlameEffectSO.OnFlameSwitchOff -= SwitchOff;
        FlameEffectSO.OnFlameSwitchOn -= SwitchOn;
    }
}
