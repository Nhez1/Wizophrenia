using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spell Effects/Main/Flame")]
public class FlameEffectSO : EffectSO
{
    [SerializeField] private GameEvent _onFlameSwitchOn;
    [SerializeField] private GameEvent _onFlameSwitchOff;

    [SerializeField] private CooldownEffect _cooldown;
    [SerializeField] private ManaSpendEffect _drain;
    private SpellSO _self;
    private CastContext _context;

    public override void Init(CastContext castContext)
    {
        _switch = false;
        _context = castContext;
        _self = castContext.Spell;
        _self.canCast = true;

        _cooldown.Init(_context);
        _drain.Init(_context);

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

    #region FlameSwitchLogic
    private bool _switch;

    void SwitchFlame()
    {
        _switch = !_switch;

        if (_switch) FlameOn();
        else FlameOff("TurnOff");
    }

    void FlameOn()
    {
        _onFlameSwitchOn.Raise(this, null);
        _context.SpellPrefab.SetActive(true);
        // Set Light in Hand active

        _drain.OnCast();
    }

    void FlameOff(string mode)
    {
        _switch = false;
        _onFlameSwitchOff.Raise(this, mode);
        _context.SpellPrefab.SetActive(false); // Set Light in Hand off

        _drain.OnCast();
    }

    void ForceOff()
    {
        FlameOff("ForceOff");

        _cooldown.OnCast();
    }
    #endregion

    void CheckMana(float mana)
    {
        if (mana <= _self.manaCost)
        {
            FlameOff("TurnOff");
            _context.Spell.canCast = false;
        }
        else
        {
            _context.Spell.canCast = true;

        }
    }
}