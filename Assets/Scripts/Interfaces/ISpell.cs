using UnityEngine;

public interface ISpell
{
    string Name { get; }
    float ManaCost { get; }


    void Init(Mana mana, GameObject prefab);

    void Cast();
}
