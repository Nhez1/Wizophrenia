using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spell")]
public class SpellSO : ScriptableObject
{
    [Header("Spell Data")]
    public string spellName;
    public float cooldown;
    public float manaCost;
    public bool canCast;
    public bool onCD = false;
    public SpellType type;

    [Header("Extra")]
    public GameObject prefab;
    public List<EffectSO> effects = new();

    public void Init(MonoBehaviour cR, Mana m, Transform handPos, GameObject gameObject = null)
    {
        foreach (var effect in effects) effect.Init(new CastContext(cR, m, handPos, gameObject, this));
    }

    public void Cast()
    {
        foreach (var effect in effects) effect.OnCast();
    }

    public void Dispose()
    {
        foreach (var effect in effects) effect.Dispose();
    }
}
