using UnityEngine;

public abstract class EffectSO : ScriptableObject
{
    public virtual void Init() { }
    public virtual void OnCast(CastContext castContext = null) { }
}

public class CastContext
{
    public MonoBehaviour CoroutineRunner;
    public Mana Mana;
    public Transform SpawnPoint;
    public GameObject SpellPrefab;
    public SpellSO Spell;

    public CastContext(MonoBehaviour mb, Mana m, Transform spawnPoint, GameObject prefab, SpellSO self)
    {
        CoroutineRunner = mb;
        Mana = m;
        SpawnPoint = spawnPoint;
        SpellPrefab = prefab;
        Spell = self;
    }
}