using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Effects/FlameEffectOFF")]
public class FlameOffEffect : EffectSO
{
    public FlameEffectSO flame;
    private GameObject _lightInHand;

    public override void Init(CastContext castContext)
    {
        _lightInHand = castContext.SpellPrefab;
    }

    public override void OnCast()
    {
        _lightInHand.SetActive(false);
    }
}
