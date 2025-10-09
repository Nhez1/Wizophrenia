using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandler : MonoBehaviour
{
    public Animator animator;
    private HandsView _view;

    private void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();

        _view = new(animator);

        ShadowHand.ForceFlameOff += _view.ShadowHandOff;
        FlameEffectSO.OnFlameSwitchOff += FlameSwitchOff;
        FlameEffectSO.OnFlameSwitchOn += FlameSwitchOn;
    }

    public void FlameSwitchOff() => _view.SwitchFlameSpell(false);
    public void FlameSwitchOn() =>_view.SwitchFlameSpell(true);

    private void OnDisable()
    {
        ShadowHand.ForceFlameOff -= _view.ShadowHandOff;
        FlameEffectSO.OnFlameSwitchOff -= FlameSwitchOff;
        FlameEffectSO.OnFlameSwitchOn -= FlameSwitchOn;
    }
}