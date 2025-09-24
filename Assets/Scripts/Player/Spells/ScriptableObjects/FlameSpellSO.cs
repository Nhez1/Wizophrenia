using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spells/Main/FlameSpell")]
public class FlameSpellSO : ScriptableObject
{
    public static event Action<float> OnFlameSwitch;
    public float manaCost;
    public bool isActive = false;

    public void FlameCast(Mana m, GameObject lightInHand)
    {
        OnFlameSwitch?.Invoke(1f);
        ActivateLight(lightInHand);
        SpendMana(m);
    }

    void SpendMana(Mana mana) => mana.Spend(manaCost);

    void ActivateLight(GameObject lih)
    {
        isActive = !isActive;

        lih.SetActive(isActive);
    }
}
