using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spellPrefabs;
    private List<ISpell> _spells;
    private Mana _mana;

    private void Awake()
    {
        _mana = GetComponent<Player>().Mana;
    }

    public GameObject GetSpellPrefab(int index)
    {
        if (index < 0 || index >= _spellPrefabs.Count)
            return null;
        return _spellPrefabs[index];
    } // Esto está así por ahora pero lo voy a cambiar más adelante.
}
