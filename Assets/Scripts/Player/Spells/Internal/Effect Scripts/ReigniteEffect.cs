using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/ExpansiveWave")]
public class ReigniteEffect : EffectSO
{
    Transform _spawnPoint;

    public override void Init(CastContext castContext)
    {
        _spawnPoint = castContext.SpawnPoint;
    }

    public override  void OnCast()
    {
        var wave = ExpansiveWaveFactory.Instance.GetExpansiveWave();
        wave.transform.SetPositionAndRotation(_spawnPoint.position, Camera.main.transform.rotation);
    }
}