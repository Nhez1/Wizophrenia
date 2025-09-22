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

    public void CastSpell(SpellType spell)
    {
        if (_spells.ContainsKey(spell))
            _spells[spell].Cast();
        else
            Debug.LogWarning($"Spell {spell} not found!");
    }

    public void AddSpell(SpellType spell)
    {
        if (_spells.ContainsKey(spell)) return;
        switch (spell)
        {
            case SpellType.FlameSpell:
                //Se agrega el hechizo al diccionario
                _spells.Add(spell, reference.AddComponent<FlameSpell>());
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
