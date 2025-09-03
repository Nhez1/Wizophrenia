using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [Tooltip("Acá van los prefabs de los hechizos que los requieran.")]
    [SerializeField] private List<GameObject> _spellPrefabs;
    private List<ISpell> _spells;
    private Mana _mana;

    private void Awake()
    {
        _mana = GetComponent<Player>().Mana;
        _spells = new List<ISpell>();

        for (int i = 0; i < _spellPrefabs.Count; i++)
        {
            if (_spells.Count > 0)
            {
                _spells[i].Init(_mana, _spellPrefabs[i]);
            }
        }
    }
}
