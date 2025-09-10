using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    FlameSpell,
    FireBall
}

public class SpellManager
{
    private static Dictionary<SpellType, ISpell> _spells = new();
    private Mana _mana;
    GameObject reference;

    public SpellManager(Mana m, GameObject g)
    {
        _mana = m;
        reference = g;
    }

    /// <summary>
    /// Reduce the player's maximum MP by a specified amount.
    /// </summary>
    /// <param name="spell">The SpellType you want to cast. Keep in mind you have to unlock the spell for the Wizard using AddSpell before being able to use this.</param>
    public void CastSpell(SpellType spell)
    {
        if (_spells.ContainsKey(spell))
            _spells[spell].Cast();
        else
            Debug.LogWarning($"Spell {spell} not found!");
    }

    /// <summary>
    /// Unlock a spell for the Wizard.
    /// </summary>
    /// <param name="spell">The SpellType you want to add. Keep in mind that for your spell to be in SpellTypes, you need to add it manually to the SpellManager script.</param>
    /// <param name="castPos">This parameter is optional. Here you pass a Transform that contains where the spell's prefab will be Instantiated.</param>
    public void AddSpell(SpellType spell)
    {
        if (_spells.ContainsKey(spell)) return;
        switch (spell)
        {
            case SpellType.FlameSpell:
                //Se agrega el hechizo al diccionario
                _spells.Add(spell, new FlameSpell());
                //Se lo inicializa dándole la referencia de mana
                _spells[spell].Init(_mana);
                Debug.Log("Added flame!");
                break;
            case SpellType.FireBall:
                _spells.Add(spell, reference.AddComponent<FireSpell>());
                _spells[spell].Init(_mana, PrefabManager.GetPrefab(PrefabType.BallOfFire));
                break;
            default:
                Debug.LogWarning($"There is no {spell} yet.");
                break;
        }
    }
}
