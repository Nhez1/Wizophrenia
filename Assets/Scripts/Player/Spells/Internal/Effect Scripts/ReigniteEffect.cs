using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/Reignite")]
public class ReigniteEffect : EffectSO
{
    CastContext _context;
    SpellSO _self;

    public float destroyRadius = 5f;

    public override void Init(CastContext castContext)
    {
        _self = castContext.Spell;
        _context = castContext;

        FlameEffectSO.OnFlameSwitchOn += SwitchOn;
        FlameEffectSO.OnFlameSwitchOff += SwitchOff;
        ShadowHand.ForceFlameOff += SwitchOff;
    }

    public override void OnCast()
    {
        var spawnPoint = _context.SpawnPoint.position;
        var wave = ExpansiveWaveFactory.Instance.GetExpansiveWave();
        wave.transform.SetPositionAndRotation(spawnPoint, Camera.main.transform.rotation);

        // 🔥 Buscar todas las ShadowHand activas en la escena
        ShadowHand[] hands = GameObject.FindObjectsOfType<ShadowHand>();
        foreach (ShadowHand hand in hands)
        {
            float dist = Vector3.Distance(spawnPoint, hand.transform.position);

            if (dist <= destroyRadius)
            {
                Debug.Log("🔥 ShadowHand destruida por Reignite!");
                GameObject.Destroy(hand.gameObject);
            }
        }
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
