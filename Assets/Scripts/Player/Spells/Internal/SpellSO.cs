using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spell")]
public class SpellSO : ScriptableObject
{
    protected MonoBehaviour _coroutineRunner;

    [Header("Spell Data")]
    public string spellName;
    public float cooldown;
    public float manaCost;
    public bool canCast;

    [Header("Extra")]
    public GameObject prefab;
    public List<EffectSO> effects = new();

    public void Init(MonoBehaviour cR)
    {
        canCast = true;
        _coroutineRunner = cR;
        foreach (var effect in effects) effect.Init();
    }

    public void Cast(Mana m, Transform spawnPoint = null)
    {
        foreach (var effect in effects)
        {
            if (canCast) effect.OnCast(new CastContext(_coroutineRunner, m, spawnPoint, prefab, this));
        }
    }

    public void Dispose()
    {
        foreach (var effect in effects) effect.Dispose();
    }
}
