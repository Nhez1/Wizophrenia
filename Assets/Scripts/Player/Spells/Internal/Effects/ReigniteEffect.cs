using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/ExpansiveWave")]
public class ReigniteEffect : EffectSO
{
    public override  void OnCast(CastContext castContext)
    {
        Instantiate(castContext.SpellPrefab, castContext.SpawnPoint.position, Camera.main.transform.rotation);
    }
}