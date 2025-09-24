using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/SpellEffects/Flame")]
public class FlameEffectSO : EffectSO
{
    public static event Action<float> OnFlameSwitch;

    public override void OnCast(GameObject x) => OnFlameSwitch?.Invoke(1f);
    // 1f is the manaCostPerSecond
}
