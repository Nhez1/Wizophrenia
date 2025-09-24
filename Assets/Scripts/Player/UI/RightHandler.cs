using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandler : MonoBehaviour
{
    public Animator animator;
    private bool _activateFlame = false;

    public void FlameSwitch(float x)
    {
        _activateFlame = !_activateFlame;
        animator.SetBool("isActive", _activateFlame);
    }

    private void OnEnable()
    {
        FlameEffectSO.OnFlameSwitch += FlameSwitch;
    }

    private void OnDisable()
    {
        FlameEffectSO.OnFlameSwitch -= FlameSwitch;
    }
}
