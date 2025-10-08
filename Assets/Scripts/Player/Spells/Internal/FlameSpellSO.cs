using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MainSpells/FlameSpell")]
public class FlameSpellSO : SpellSO, IDisposable
{
    public EffectSO cd;
    public bool isActive;

    GameObject lightInHand;
    Mana mana;

    public void FlameInit(Mana m, GameObject liH)
    {
        isActive = false;
        ShadowHand.ForceFlameOff += ForceOff;
        Mana.OnManaChanged += CheckMana;
        lightInHand = liH;
        mana = m;
    }

    public void FlameCast()
    {
        if (canCast)
        {
            LightSwitch(lightInHand);
            Cast(); //Re pete que tenga que depender del script padre pero bueno, ya estoy re quemado, no quiero hacer más esto
        }
    }

    void ForceOff()
    {
        if (isActive)
        {
            FlameCast();
            // Bien hardocdeado como Dios manda
        }
        else return;
    }

    void LightSwitch(GameObject lih)
    {
        isActive = !isActive;
        lih.SetActive(isActive);
    }

    void CheckMana(float mana)
    {
        if (mana <= manaCost) ForceOff();
        else canCast = true;
    }

    public void FlameDispose()
    {
        ShadowHand.ForceFlameOff -= FlameCast;
        Mana.OnManaChanged -= CheckMana;
    }
}

