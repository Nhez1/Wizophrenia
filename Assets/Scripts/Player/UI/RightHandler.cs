using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandler : MonoBehaviour
{
    public Animator animator;
    private bool _activateFlame = false;

    public void FlameSwitch()
    {
        _activateFlame = !_activateFlame;
        animator.SetBool("isActive", _activateFlame);
    }

    private void OnEnable()
    {
        FlameSpellSO.OnFlameSwitch += FlameSwitch;
    }

    private void OnDisable()
    {
        FlameSpellSO.OnFlameSwitch -= FlameSwitch;
    }
}
