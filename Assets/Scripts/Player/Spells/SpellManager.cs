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
    FlameSpellSO flameSpell;
    private static Dictionary<SpellType, SpellSO> _spells = new();
    private Mana _mana;
    Transform castPoint;
    MonoBehaviour coroutineStarter;

    public SpellManager(Mana m, GameObject lightInHand, Transform castPosition, MonoBehaviour mb, FlameSpellSO flame)
    {
        _mana = m;
        castPoint = castPosition;
        coroutineStarter = mb;

        // Este hechizo se inicializa aparte, va a ser la única excepción a la lista.
        flameSpell = flame;
        flameSpell.Init(mb);
        flameSpell.FlameInit(m, lightInHand);
    }

    /// <summary>
    /// Cast spell with indicated SpellType.
    /// </summary>
    /// <param name="spell">The SpellType you want to cast. Keep in mind you have to unlock the spell for the Wizard using AddSpell before being able to use this.</param>
    public void CastSpell(SpellType spell)
    {
        if (spell == SpellType.FlameSpell) flameSpell.FlameCast();
        else
        {
            if (_spells.ContainsKey(spell))
                _spells[spell].Cast(_mana, castPoint);
            else
                Debug.LogWarning($"Spell {spell} not found!");
        }
    }

    /// <summary>
    /// Unlock a spell for the Wizard.
    /// </summary>
    /// <param name="spellType">The SpellType you want to add. Keep in mind that for your spell to be in SpellTypes, you need to add it manually to the SpellManager script.</param>
    /// <param name="spell">This is the SpellSO containing the spell you want to give the Wizard. Keep in mind that every spell needs to be a SpellSO.</param>
    public void AddSpell(SpellType spellType, SpellSO spell)
    {
        if (_spells.ContainsKey(spellType)) return;

        _spells.Add(spellType, spell);
        _spells[spellType].Init(coroutineStarter);
        Debug.Log("The Wizard has learned " + spellType.ToString());
    }

    public void SpellDispose()
    {
        flameSpell.FlameDispose();
        
        foreach(var spell in _spells.Values) spell.Dispose();
    }
}
