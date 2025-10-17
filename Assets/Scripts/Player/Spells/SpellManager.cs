using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    FlameSpell,
    FireBall,
    Exorcise
}

public class SpellManager
{
    private static Dictionary<SpellType, SpellSO> _spells = new();
    private Mana _mana;
    Transform castPoint;
    MonoBehaviour _coroutineStarter;
    private Dictionary<SpellType, GameObject> _spellPrefabs = new();

    public void RegisterSpellPrefab(SpellType type, GameObject prefab)
    {
        _spellPrefabs[type] = prefab;
    }

    public SpellManager(Mana m, Transform castPosition, MonoBehaviour mb)
    {
        _mana = m;
        castPoint = castPosition;
        _coroutineStarter = mb;
    }

    /// <summary>
    /// Cast spell with indicated SpellType.
    /// </summary>
    /// <param name="spellToCast">The SpellType you want to cast. Keep in mind you have to unlock the spell for the Wizard using AddSpell before being able to use this.</param>
    public void CastSpell(SpellType spellToCast)
    {
        var spell = _spells[spellToCast];

        if (_spells.ContainsKey(spellToCast))
        {
            if (spell.canCast && !spell.onCD && _mana.MP > spell.manaCost) spell.Cast();
        }
        else
            Debug.LogWarning($"Spell {spellToCast} not found!");
    }

    /// <summary>
    /// Unlock a spell for the Wizard.
    /// </summary>
    /// <param name="spell">This is the SpellSO containing the spell you want to give the Wizard. Keep in mind that every spell needs to be a SpellSO.</param>
    public void AddSpell(SpellSO spell)
    {
        if (_spells.ContainsKey(spell.type)) return;

        _spells.Add(spell.type, spell);
        var prefab = _spellPrefabs.TryGetValue(spell.type, out var p) ? p : null;
        _spells[spell.type].Init(_coroutineStarter, _mana, castPoint, prefab);
        Debug.Log("The Wizard has learned " + spell.type.ToString());
    }

    public void SpellDispose()
    {
        foreach (var spell in _spells.Values) spell.Dispose();
    }
}
