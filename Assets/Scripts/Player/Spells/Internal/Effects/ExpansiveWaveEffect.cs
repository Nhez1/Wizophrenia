using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/ExpansiveWave")]
public class ExpansiveWaveEffect : EffectSO
{
    public override  void OnCast(CastContext castContext)
    {
        Instantiate(castContext.SpellPrefab, castContext.SpawnPoint.position, Camera.main.transform.rotation);
    }
}