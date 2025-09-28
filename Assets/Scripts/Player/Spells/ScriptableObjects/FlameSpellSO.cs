using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MainSpells/FlameSpell")]
public class FlameSpellSO : SpellSO
{
    public static event Action OnFlameSwitch;
    public bool isActive = false;

    public void FlameCast(Mana m, GameObject lightInHand)
    {
        OnFlameSwitch?.Invoke();
        ActivateLight(lightInHand);
        SpendMana(m);
    }

    void SpendMana(Mana mana) => mana.Drain(manaCost);

    void ActivateLight(GameObject lih)
    {
        isActive = !isActive;
        Debug.Log("Set light to " + isActive);
        lih.SetActive(isActive);
    }
}
