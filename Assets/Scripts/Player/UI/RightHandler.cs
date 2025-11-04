using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandler : MonoBehaviour
{
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private Animator _anim;
    private HandsAnimator _view;

    private void Start()
    {
        if (_anim == null) _anim = GetComponent<Animator>();

        _view = new(_anim);

        ShadowHand.ForceFlameOff += _view.ShadowHandOff;
        FlameEffectSO.OnFlameSwitchOff += FlameSwitchOff;
        FlameEffectSO.OnFlameSwitchOn += FlameSwitchOn;
    }

    public void FlameSwitchOff() => _view.SwitchFlameSpell(false);
    public void FlameSwitchOn() => _view.SwitchFlameSpell(true);

    public void OccupyHandSlot(object sender, object data)
    {
        if (data is ItemSO item)
        {
            if (item.type == ItemType.Herb) _leftHand.SetActive(true);
        }
    }

    public void ClearHandSlot(object sender, object data)
    {
        _leftHand.SetActive(false);
    }

    private void OnDisable()
    {
        ShadowHand.ForceFlameOff -= _view.ShadowHandOff;
        FlameEffectSO.OnFlameSwitchOff -= FlameSwitchOff;
        FlameEffectSO.OnFlameSwitchOn -= FlameSwitchOn;
    }
}