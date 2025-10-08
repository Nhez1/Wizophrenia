using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/MainSpells/Flame")]
public class FlameSO : EffectSO
{
    public static event Action OnFlameSwitchOn;
    public static event Action OnFlameSwitchOff;

    public CooldownEffect cooldown;
    private GameObject lightInHand;
    private SpellSO root;
    public float manaCost;
    bool flameSwitch = true;

    public override void Init(CastContext castContext)
    {
        cooldown.Init(castContext);
        root = castContext.Spell;
        lightInHand = castContext.SpellPrefab;
        ShadowHand.ForceFlameOff += ForceOff;
        Mana.OnManaChanged += CheckMana;
    }

    public override void OnCast()
    {
        SwitchFlame();
    }

    public override void Dispose()
    {
        ShadowHand.ForceFlameOff -= ForceOff;
        Mana.OnManaChanged -= CheckMana;
    }

    void SwitchFlame()
    {
        flameSwitch = !flameSwitch;
        if (flameSwitch) FlameOn();
        else FlameOff();
    }

    public void FlameOn()
    {
        OnFlameSwitchOn?.Invoke();
        lightInHand.SetActive(true);
    }

    public void FlameOff()
    {
        OnFlameSwitchOff?.Invoke();
        lightInHand.SetActive(false);
    }

    void ForceOff()
    {
        FlameOff();
        cooldown.OnCast();
    }

    void CheckMana(float mana)
    {
        if (mana <= manaCost)
        {
            FlameOff();
            root.canCast = false;
        }
        else root.canCast = true;
    }
}