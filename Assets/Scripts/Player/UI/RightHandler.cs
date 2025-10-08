using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandler : MonoBehaviour
{
    public Animator animator;
    private HandsView _view;
    private bool _activateFlame = false;

    private void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();

        _view = new(animator);

        ShadowHand.ForceFlameOff += _view.ShadowHandOff;
        FlameSO.OnFlameSwitchOff += FlameSwitchOff;
        FlameSO.OnFlameSwitchOn += FlameSwitchOn;
    }

    public void FlameSwitchOff()
    {
        _activateFlame = false;
        _view.SwitchFlameSpell(_activateFlame);
    }

    void FlameSwitchOn()
    {
        _activateFlame = true;
        _view.SwitchFlameSpell(_activateFlame);
    }

    private void OnDisable()
    {
        ShadowHand.ForceFlameOff -= _view.ShadowHandOff;
        FlameSO.OnFlameSwitchOff -= FlameSwitchOff;
        FlameSO.OnFlameSwitchOn -= FlameSwitchOn;
    }
}