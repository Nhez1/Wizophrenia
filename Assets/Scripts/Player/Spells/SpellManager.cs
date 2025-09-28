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
    private FlameSpellSO flameSpell;
    private SpellSO fireSpell;
    // CHANGE LATER

    private static Dictionary<SpellType, SpellSO> _spells = new();
    private Mana _mana;
    GameObject lightInHand;
    Transform castPoint;
    MonoBehaviour coroutineStarter;

    public SpellManager(Mana m, GameObject g, Transform castPosition, MonoBehaviour mb, FlameSpellSO flame, SpellSO fire)
    {
        _mana = m;
        lightInHand = g;
        castPoint = castPosition;
        coroutineStarter = mb;
        flameSpell = flame;
        fireSpell = fire;
    }

    /// <summary>
    /// Reduce the player's maximum MP by a specified amount.
    /// </summary>
    /// <param name="spell">The SpellType you want to cast. Keep in mind you have to unlock the spell for the Wizard using AddSpell before being able to use this.</param>
    public void CastSpell(SpellType spell)
    {
        if (_spells.ContainsKey(spell))
        {
            if (spell == SpellType.FlameSpell) flameSpell.FlameCast(_mana, lightInHand);
            _spells[spell].Cast(_mana, castPoint);
        }
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
                _spells.Add(spell, flameSpell);
                Debug.Log("The Wizard has learned Flame!");
                break;
            case SpellType.FireBall:
                _spells.Add(spell, fireSpell);
                _spells[spell].Init(coroutineStarter);
                Debug.Log("The Wizard has learned Fire Ball!");
                break;
            default:
                Debug.LogWarning($"There is no {spell} yet.");
                break;
        }
    }
}
