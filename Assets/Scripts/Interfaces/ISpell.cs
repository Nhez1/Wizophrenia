using UnityEngine;

// "Passive" spells will be the ones that do not use a prefab, whereas "Active" spells will requiere a prefab.

public interface ISpell
{
    string Name { get; }
    float ManaCost { get; }

    void Init(Mana mana, GameObject prefab = null);
    void Cast();
}