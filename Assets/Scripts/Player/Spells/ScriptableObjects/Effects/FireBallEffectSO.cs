using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/FireBall")]
public class FireBallEffectSO : EffectSO
{
    bool canShoot = false;

    public override void Init() => FlameSpellSO.OnFlameSwitch += SwitchActive;

    public override void OnCast(CastContext castContext)
    {
        if (canShoot) Instantiate(castContext.SpellPrefab, castContext.SpawnPoint.position, Camera.main.transform.rotation);
        else return;
    }

    void SwitchActive() => canShoot = !canShoot;
}
