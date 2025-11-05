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
    }

    public void FlameOff(object sender, object data)
    {
        string mode = data as string;

        if (mode is "TurnOff") HUDFlameOff();
        else if (mode is "ForceOff") _view.ShadowHandOff();
    }
    public void FlameSwitchOn() => _view.SwitchFlameSpell(true);
    public void HUDFlameOff() => _view.SwitchFlameSpell(false);

    public void OccupyHandSlot(object sender, object data)
    {
        if (data is ItemSO item)
        {
            if (item.type == ItemType.Herb) _leftHand.SetActive(true);
        }
    }

    public void ClearHandSlot()
    {
        _leftHand.SetActive(false);
    }
}