using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MainSpells/Flame")]
public class FlameEffectSO : EffectSO
{
    public static event Action OnFlameSwitchOn;
    public static event Action OnFlameSwitchOff;

    private SpellSO _self;
    private CastContext _context;
    public CooldownEffect cooldown;
    public ManaSpendEffect drain;
    bool _switch;

    public override void Init(CastContext castContext)
    {
        _switch = false;
        _context = castContext;
        _self = castContext.Spell;
        _self.canCast = true;

        cooldown.Init(_context);
        drain.Init(_context);

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
        _switch = !_switch;

        if (_switch) FlameOn();
        else FlameOff();
    }

    void FlameOn()
    {
        OnFlameSwitchOn?.Invoke();
        _context.SpellPrefab.SetActive(true);
        // Set Light in Hand active

        drain.OnCast();
    }

    void FlameOff()
    {
        OnFlameSwitchOff?.Invoke();
        _context.SpellPrefab.SetActive(false); // Set Light in Hand off

        drain.OnCast();
    }

    void ForceOff()
    {
        _switch = false;
        _context.SpellPrefab.SetActive(false); // Set Light in Hand off

        drain.OnCast();
        cooldown.OnCast();
    }

    void CheckMana(float mana)
    {
        if (mana <= _self.manaCost)
        {
            FlameOff();
            _context.Spell.canCast = false;
        }
        else return;
    }
}