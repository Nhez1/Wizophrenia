using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spell Effects/Reignite")]
public class ReigniteEffect : EffectSO
{
    private CastContext _context;
    private SpellSO _self;

    public override void Init(CastContext castContext)
    {
        _self = castContext.Spell;
        _context = castContext;
    }

    public override void OnCast()
    {
        var spawnPoint = _context.SpawnPoint.position;
        var wave = ExpansiveWaveFactory.Instance.GetExpansiveWave();
        wave.transform.SetPositionAndRotation(spawnPoint, Camera.main.transform.rotation);

        // 🔥 Buscar todas las ShadowHand activas en la escena
        ShadowHand[] hands = FindObjectsOfType<ShadowHand>();
        foreach (ShadowHand hand in hands)
        {
            float dist = Vector3.Distance(spawnPoint, hand.transform.position);
            if (dist <= 5) hand.gameObject.SetActive(false);
        }
    }

    public void SwitchOff() => _self.canCast = false;
    public void SwitchOn() => _self.canCast = true;
}
