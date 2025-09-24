using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spell")]
public class SpellSO : ScriptableObject
{
    public string spellName;
    public float cooldown;
    public float manaCost;
    public GameObject prefab;
    public List<EffectSO> effects = new();

    public void Cast(Mana m, Transform spawnPoint)
    {
        foreach(var effect in effects)
        {
            effect.OnCast(prefab, spawnPoint);
            SpendMana(m);
        }
    }

    void SpendMana(Mana mana) => mana.Spend(manaCost);
}
