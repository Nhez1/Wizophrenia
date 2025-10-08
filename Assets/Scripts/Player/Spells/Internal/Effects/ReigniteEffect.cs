using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/ExpansiveWave")]
public class ReigniteEffect : EffectSO
{
    GameObject prefab;
    Transform spawnPoint;

    public override void Init(CastContext castContext)
    {
        prefab = castContext.SpellPrefab;
        spawnPoint = castContext.SpawnPoint;
    }

    public override  void OnCast()
    {
        Instantiate(prefab, spawnPoint.position, Camera.main.transform.rotation);
    }
}