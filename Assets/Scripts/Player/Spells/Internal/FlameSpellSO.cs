using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MainSpells/FlameSpell")]
public class FlameSpellSO : SpellSO, IDisposable
{
    public static event Action OnFlameSwitch;
    public EffectSO cd;
    public bool isActive;

    GameObject lightInHand;
    Mana mana;

    public void FlameInit(Mana m, GameObject liH)
    {
        isActive = false;
        GhostTOL.ForceFlameOff += ForceOff;
        Mana.OnManaChanged += CheckMana;
        lightInHand = liH;
        mana = m;
    }

    public void FlameCast()
    {
        if (canCast)
        {
            OnFlameSwitch?.Invoke();
            LightSwitch(lightInHand);
            Cast(mana); //Re pete que tenga que depender del script padre pero bueno, ya estoy re quemado, no quiero hacer más esto
        }
    }

    void ForceOff()
    {
        if (isActive)
        {
            FlameCast();
            cd.OnCast(new CastContext(_coroutineRunner, null, null, null, this)); 
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
        GhostTOL.ForceFlameOff -= FlameCast;
        Mana.OnManaChanged -= CheckMana;
    }
}
