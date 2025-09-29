using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/FireBall")]
public class FireBallEffectSO : EffectSO
{
    bool canShoot;

    public override void Init()
    {
        canShoot = false;
        FlameSpellSO.OnFlameSwitch += SwitchActive;
    }

    public override void OnCast(CastContext castContext)
    {
        if (canShoot) Instantiate(castContext.SpellPrefab, castContext.SpawnPoint.position, Camera.main.transform.rotation);
        else return;
    }

    void SwitchActive() => canShoot = !canShoot;

    public override void Dispose() => FlameSpellSO.OnFlameSwitch -= SwitchActive;
}
