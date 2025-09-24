using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spells")]
public class SpellSO : ScriptableObject
{
    public string spellName;
    public float cooldown;
    public float manaCost;
    public GameObject prefab;
    public List<EffectSO> effects = new();

    public void Cast(Mana m)
    {
        foreach(var effect in effects)
        {
            effect.OnCast(prefab);
            SpendMana(m);
        }
    }

    void SpendMana(Mana mana) => mana.Spend(manaCost);
}
