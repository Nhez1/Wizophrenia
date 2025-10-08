using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/MainSpells/Flame")]
public class FlameEffectSO : EffectSO
{
    public static event Action OnFlameSwitchOn;
    public static event Action OnFlameSwitchOff;

    public float manaCost;
    public CooldownEffect cooldown;
    private CastContext _castContext;
    bool flameSwitch;

    public override void Init(CastContext castContext)
    {
        flameSwitch = false;
        cooldown.Init(castContext);
        _castContext = castContext;
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

    void DrainMana()
    {
        _castContext.Mana.Drain(manaCost);
    }

    void SwitchFlame()
    {
        flameSwitch = !flameSwitch;
        Debug.Log("Set flame " + flameSwitch);

        if (flameSwitch) FlameOn();
        else FlameOff();
    }

    void FlameOn()
    {
        Debug.Log("Flame on");
        OnFlameSwitchOn?.Invoke();
        _castContext.SpellPrefab.SetActive(true);
        // Set Light in Hand active

        DrainMana();
    }
    void FlameOff()
    {
        OnFlameSwitchOff?.Invoke();
        _castContext.SpellPrefab.SetActive(false);
        // Set Light in Hand off

        DrainMana();
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
            _castContext.Spell.canCast = false;
        }
        else _castContext.Spell.canCast = true;
    }
}