using UnityEngine;

public abstract class EffectSO : ScriptableObject
{
    public virtual void Init(CastContext castContext = null) { }
    public virtual void OnCast() { }
    public virtual void Dispose() { }
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